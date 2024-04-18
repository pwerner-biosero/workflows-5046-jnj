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
    public class SetWorklistOutput
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
           	var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");

            	await context.UpdateGlobalVariableAsync("Output.CONTAINER_WORKLIST", container_worklist);
            
            	//return Task.CompletedTask;
           
            
        }
    }
}



