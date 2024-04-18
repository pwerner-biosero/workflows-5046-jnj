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
    public class GetBarcode
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var jsonString = context.GetGlobalVariableValue<string>("PickListJSON");
            var index = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
            var pickListData = BarcodeManager.GetPickListDataAtIndex(jsonString, index);
            
            if (pickListData.HasValue)
            {
                var (barcode, picklist) = pickListData.Value;
                
                await context.UpdateGlobalVariableAsync("BARCODE", barcode, cancellationToken);
                await context.UpdateGlobalVariableAsync("PICKLIST", picklist, cancellationToken);
                
                log.Information($"Generating Picklist for Barcode: {barcode}");
                Console.WriteLine(picklist);
            }
            else
            {
                log.Information("Picklist data not found for the given index.");
            }

           // return Task.CompletedTask;
        }
    }
}