using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;

namespace Acme.Orchestrator.Scripting
{
    public class GetRoomTempContainers
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var container_list = context.GetGlobalVariableValue<string>("STORAGE_RT_STORE_1_AMBIENT");

            log.Information(container_list);

            string result = GetContainerTypes(container_list);
            
            string[] splitResult = result.Split(',');
            
            await context.UpdateGlobalVariableAsync("RT_NUMBER_OF_CONTAINERS",  splitResult.Length);
            
            
            
        }

        public static string GetContainerTypes(string csvData)
        {
            var lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var containerTypes = lines
                .Skip(1) // Skip header
                .Select(line => line.Split(','))
                .Where(columns => columns.Length > 8 && columns[7].Trim() == "1")
                .Select(columns => columns[1].Trim())
                .ToList();

            return string.Join(", ", containerTypes);
        }

    }
}
