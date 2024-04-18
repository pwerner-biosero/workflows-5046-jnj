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
    public class ParseInput
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {

             var sample_list = context.GetGlobalVariableValue<string>("SAMPLE_LIST");
		 
	     var storage_requirement =  IntelligentBatchingServiceClient.GetStorageRequirement(sample_list);
	     
	     context.UpdateGlobalVariableAsync("STORAGE_REQUIREMNT", storage_requirement);
	     
	     log.Information($"STORAGE REQUIREMENT: {storage_requirement}");

	
            return Task.CompletedTask;
        }
            
  
    }
    

}
