using System;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using System.Collections.Generic;
using Serilog;

namespace Acme.Orchestrator.Scripting
{
    public class SetBarcodes
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
            
            var loop_counter = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
            var updated_worklist = MetaDataProcessor.SetBarcode("SIMULTATE", container_worklist, loop_counter );
            
            await context.UpdateGlobalVariableAsync("Input.CONTAINER_WORKLIST", updated_worklist);

        }
  
    }
}
