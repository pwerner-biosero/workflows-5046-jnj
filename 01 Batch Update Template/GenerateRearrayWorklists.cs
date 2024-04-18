using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using CalLabUtilities;
using Serilog;

namespace Acme.Orchestrator.Scripting
{
    public class GenerateRearrayWorklists
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var storageVariables = new List<string>
            {
                "STORAGE_AZENTA_MINUS_80",
                "STORAGE_AZENTA_MINUS_20",
                "STORAGE_VERSO_Q20_1",
                "STORAGE_VERSO_Q20_2",
                "STORAGE_REARRAY_1_AMBIENT"
            };

            var storageValues = GetNonEmptyGlobalVariableValues(context, storageVariables);

            var (pickLists, anchorBarcodes) = BarcodeManager.BuildPickLists(storageValues);

            List<string> anchorBarcodeList = new List<string>();

            foreach (var pickList in pickLists)
            {
                Console.WriteLine($"Picklist for {pickList.Key} (Anchor Barcode: {anchorBarcodes[pickList.Key]}):");
                Console.WriteLine(pickList.Value);
                Console.WriteLine();

                // Add the anchor barcode to the list
                anchorBarcodeList.Add(anchorBarcodes[pickList.Key]);
            }

            // Concatenate all anchor barcodes into a single string
            var anchorBarcodesString = String.Join(",", anchorBarcodeList);

            // Assign the concatenated string to a global variable
            context.UpdateGlobalVariableAsync("ANCHOR_BARCODES", anchorBarcodesString);
            Console.WriteLine("ANCHOR BARCODES");
            Console.WriteLine(anchorBarcodesString);

            return Task.CompletedTask;
        }

        private List<string> GetNonEmptyGlobalVariableValues(WorkflowContext context, List<string> variableKeys)
        {
            List<string> values = new List<string>();
            foreach (var key in variableKeys)
            {
                var value = context.GetGlobalVariableValue<string>(key);
                if (!string.IsNullOrEmpty(value))
                {
                    values.Add(value);
                }
            }
            return values;
        }

        private string BuildWorklist(WorkflowContext context, string barcodeListKey, string pickListKey)
        {
            log.Information(barcodeListKey);
            var barcodeList = context.GetGlobalVariableValue<string>(barcodeListKey);
            log.Information(barcodeList);
            var pickList = context.GetGlobalVariableValue<string>(pickListKey);
            return BarcodeManager.UpdateRackBarcodes(pickList, barcodeList);
        }
    }
}
