


#r CalLabIdentities.dll
#r WorkflowHelper.dll
#r BatchingServiceClient.dll

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using System.IO;

using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;
using CalLabUtilities;
using Biosero.DataServices.RestClient;
using Biosero.DataModels.Ordering;
using Newtonsoft.Json;


namespace Acme.Orchestrator.Scripting
{
    public class OverideSampleJson
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {


        		string filePath = @"C:\Program Files (x86)\BioseroWorkflowService\input.json"; // Replace with the actual file path
        		string fileContents = File.ReadAllText(filePath);
        
        		 //log.Information(fileContents);
        		 context.UpdateGlobalVariableAsync("SAMPLE_LIST", fileContents);

	
            return Task.CompletedTask;
        }
            
  
    }
    

}
