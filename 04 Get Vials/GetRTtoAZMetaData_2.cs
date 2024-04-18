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
    public class GetRTtoAZMetaData_2
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            var loop_number = context.GetGlobalVariableValue<int>("AZENTA_LOOP_COUNTER");
            loop_number = loop_number-2;
            
            log.Information($"Loop number:{loop_number}");
            var csvData = context.GetGlobalVariableValue<string>("STORAGE_AZENTA_MINUS_80");
            
             var meta_data_string =  MetaDataProcessor.ProcessMetaData(csvData, loop_number);
             log.Information(meta_data_string);
             context.UpdateGlobalVariableAsync("RT_TO_AZENTA_META_DATA_2", meta_data_string);
      
            return Task.CompletedTask;
        }
    }
}

