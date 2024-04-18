#r CalLabIdentities.dll
#r WorkflowHelper.dll
using System;
using System.Linq;
using Newtonsoft.Json;
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
    public class GetContainerMetaData
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
        	    var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
            
            var loop_counter = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
            var loop_adjustment = context.GetGlobalVariableValue<int>("LOOP_ADJUSTMENT");
            
            var idx = loop_counter - loop_adjustment;
            
            var container_metadata = MetaDataProcessor.GetContainer(container_worklist, idx);
            
            var container = JsonConvert.DeserializeObject<StorageContainer>(container_metadata);
            
            var rack_barcode = container.TUBES[0].RACK_BARCODE;
            
            Console.WriteLine($"Rack Barcode: {rack_barcode}");
         
            await  context.UpdateGlobalVariableAsync("RT_STORE_BARCODE", rack_barcode);
            
            log.Information(container_metadata);

            await context.UpdateGlobalVariableAsync("PICKED_RACK_META_DATA", container_metadata);

         
        }

    }
}
