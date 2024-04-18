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
    public class SimulateBarcodes
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
    	  /*
            var meta_data = context.GetGlobalVariableValue<string>("PICKED_RACK_META_DATA");
            
            StorageContainer container = JsonConvert.DeserializeObject<StorageContainer>(meta_data);
            
            
            var firstPart = container.PROCESS_LABWARE;

            // Generate a random 5-digit number
            var random = new Random();
            var randomNumber = random.Next(10000, 99999);

            // Combine the first part and the random number to form the barcode
            var barcode = $"{firstPart}_{randomNumber}";

            // Update the global variable with the generated barcode
            context.UpdateGlobalVariableAsync("BARCODES", barcode);

            log.Information($"Generated Barcode: {barcode}");
            
            */

            return Task.CompletedTask;
        }
    }
}
