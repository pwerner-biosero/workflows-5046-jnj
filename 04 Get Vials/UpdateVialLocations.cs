#r CalLabIdentities.dll
#r WorkflowHelper.dll
using System;
using System.Linq;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using CalLabUtilities;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;


namespace Acme.Orchestrator.Scripting
{
    public class UpdateVialLocations
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
        	    var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
        	    var storage = context.GetGlobalVariableValue<string>("Input.STORAGE");
            
            var loop_counter = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
            var container_json =MetaDataProcessor.GetContainer(container_worklist, loop_counter);
            
            var worklist = MetaDataProcessor.GetVialWorklist(container_worklist, loop_counter, storage);
            
            
            var container = JsonConvert.DeserializeObject<StorageContainer>(container_json);
            
      
            
            MetaDataProcessor.UpdateVialLocationsFromStorage(worklist);
                                                 
            
       
        
            return Task.CompletedTask;
        }

    }
}
