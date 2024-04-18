using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;

namespace Acme.Orchestrator.Scripting
{
    public class GetRTReagents
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var container_list = context.GetGlobalVariableValue<string>("STORAGE_REARRAY_1_AMBIENT");
            
            log.Information("Starting RT REAGENTS");

            log.Information(container_list);

            string containerTypesResult = GetContainerTypes(container_list);
            log.Information("***************************************************");
            log.Information(containerTypesResult);

            string[] splitContainerTypesResult = containerTypesResult.Split(',');
            await context.UpdateGlobalVariableAsync("RT_REAGENTS_NUMBER_OF_CONTAINERS", splitContainerTypesResult.Length);
               log.Information("SUCCESS");

            string barcodesResult = GetBarcodesWhereRearrayIsFalse(container_list);
            await context.UpdateGlobalVariableAsync("RT_REAGENT_BARCODE_LIST", barcodesResult);
            log.Information("***************************************************");
            log.Information(barcodesResult);
        }

        public static string GetContainerTypes(string csvData)
        {
            var lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var containerTypes = lines
                .Skip(1) // Skip header
                .Select(line => line.Split(','))
                .Where(columns => columns.Length > 6 && columns[6].Trim().Equals("False", StringComparison.OrdinalIgnoreCase))
                .Select(columns => columns[1].Trim())
                .ToList();

            return string.Join(", ", containerTypes);
        }

        public static string GetBarcodesWhereRearrayIsFalse(string csvData)
        {
            var lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var barcodes = lines
                .Skip(1) // Skip header
                .Select(line => line.Split(','))
                .Where(columns => columns.Length > 6 && columns[6].Trim().Equals("False", StringComparison.OrdinalIgnoreCase))
                .Select(columns => columns[2].Trim()) // Get the third column (barcode)
                .ToList();

            return string.Join(",", barcodes);
        }
    }
}
