
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
    public class UpdateContainerBarcode
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
       
           var barcode =  context.GetGlobalVariableValue<string>("BARCODES");
           var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
           var loop_counter = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
           var idx = loop_counter;

           container_worklist = MetaDataProcessor.UpdateRackBarcode(container_worklist,idx,barcode);
           await context.UpdateGlobalVariableAsync("Input.CONTAINER_WORKLIST", container_worklist);

            //return Task.CompletedTask;
        }
        

    }
}
