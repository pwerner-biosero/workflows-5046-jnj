const fs = require('fs');

const path = require('path');

function processDirectory(directory) {
    // Skip node_modules and .vscode directories
    if (directory.includes('node_modules') || directory.includes('.vscode')) {
        return;
    }

    fs.readdir(directory, (err, items) => {
        if (err) {
            console.error(`Error reading directory ${directory}:`, err);
            return;
        }
        items.forEach(item => {
            const fullPath = path.join(directory, item);
            fs.stat(fullPath, (err, stats) => {
                if (err) {
                    console.error(`Error getting stats for ${fullPath}:`, err);
                    return;
                }
                if (stats.isDirectory()) {
                    processDirectory(fullPath);
                } else if (stats.isFile() && path.extname(fullPath) === '.wfx' && directory !== __dirname) {
                    // Only process files if we're not in the root directory
                    processFile(fullPath);
                }
            });
        });
    });
}

function processFile(file) {
    fs.readFile(file, 'utf8', (err, data) => {
        if (err) {
            console.error(`Error reading ${file}:`, err);
            return;
        }
        console.log(file);
        const scripts = JSON.parse(data);

        for (const script of scripts.scripts) {
            const name = script.name;
            const filename = path.join(path.dirname(file), `${name}.cs`);
            const code = script.code;

            fs.writeFileSync(filename, code);
        }
    });
}

// Start processing from the current directory
processDirectory(__dirname);