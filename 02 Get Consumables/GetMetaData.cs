using System;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;

using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;

namespace Acme.Orchestrator.Scripting
{
    public class GetRTMetaData
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var loop_number = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            var csvData = context.GetGlobalVariableValue<string>("Input.STORAGE_WORKLIST");
            
            var step_number = context.GetGlobalVariableValue<int>("Input.StepNumber");

            var meta_data = CSVParser.ParseCSV(csvData, step_number, false);

            var meta_data_string = $"{meta_data[loop_number].ContainerType},{meta_data[loop_number].ProcessLabware}";

            context.UpdateGlobalVariableAsync("META_DATA", meta_data_string);

            log.Information(meta_data_string);
            
            return Task.CompletedTask;
        }     
    }
}
