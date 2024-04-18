
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

using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;


namespace Acme.Orchestrator.Scripting
{
    public class GenerateStorageWorklist
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
        	    var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
        	    
        	    
        	    var storage = context.GetGlobalVariableValue<string>("Input.STORAGE");
            
            var loop_counter = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
     
            
            var worklist = MetaDataProcessor.GetVialWorklist(container_worklist, loop_counter, storage);
            
            await context.UpdateGlobalVariableAsync("WORKLIST", worklist);
        
            //return Task.CompletedTask;
        }

    }
}
