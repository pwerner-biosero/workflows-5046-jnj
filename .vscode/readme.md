
# Designer Script Parser

## Overview

`designer-script-parser` is a Node.js script that automates the extraction of C# code from Workflow Designer Files (`.wfx` files) and exports them to C# files (`.cs`). This utility is designed to streamline the process of handling C# scripts embedded within workflow configurations, improving workflow management and version control in software projects. The upcoming feature of this project will include the capability to re-import these scripts back into the workflow `.wfx` files.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed on your system:
- **Node.js**: Download it from [Node.js official website](https://nodejs.org/).
- **Visual Studio Code**: This is recommended for editing and running the script with ease. Download it from [Visual Studio Code website](https://code.visualstudio.com/).

### Installation

To set up the `designer-script-parser`, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/designer-script-parser.git
   ```
2. Navigate to the project directory:
   ```bash
   cd designer-script-parser
   ```

### Usage

To start extracting scripts, you have two options:

1. **Command Line Execution**:
   Run the following command from the root of your project directory:
   ```bash
   node index.js
   ```

2. **Using Visual Studio Code Debug Configuration**:
   - Open the project in Visual Studio Code.
   - Go to the Debug pane.
   - Select the `Extract Script` launch configuration from the dropdown.
   - Click the start button (green arrow) to begin the process.

This script will recursively process all directories, skipping `node_modules` and `.vscode`. It will specifically look for `.wfx` files to extract C# code and save it as `.cs` files in the same directory as the source file.

## How it Works

The script operates by reading through directories recursively, identifying `.wfx` files, and extracting embedded C# code. Each script within a `.wfx` file is written to a separate `.cs` file named after the script's name field. This facilitates easier maintenance and reuse of the scripts in various workflows.

## Contributions

Contributions are welcome! F
