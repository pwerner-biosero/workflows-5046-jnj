#r CalLabIdentities.dll
#r WorkflowHelper.dll
#r BatchingServiceClient.dll

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;


using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;
using CalLabUtilities;
using Biosero.DataServices.RestClient;
using Biosero.DataModels.Ordering;
using Newtonsoft.Json;



namespace Acme.Orchestrator.Scripting
{
    public class ProcessSampleUpdateTemplate
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            try
            {
                var url = "http://localhost:7183/";

                IntelligentBatchingServiceClient ibsc = new IntelligentBatchingServiceClient(url);
                
                var bc_json = context.GetGlobalVariableValue<string>("JSON_SAMPLE_LIST");
                
                var order = await client.GetOrderAsync(context.OrderIdentifier);

                var (sample_list, order_template) = WorkflowHelper.ProcessSampleUpdateTemplate(bc_json, order.Identifier);
              
                await context.UpdateGlobalVariableAsync("SAMPLE_LIST", sample_list);
                
                await context.UpdateGlobalVariableAsync("TEMPLATE_NAME", order_template);
            }
            catch (Exception ex)
            {
                log.Error($"An error occurred during the process: {ex.Message}");
                throw; // Re-throwing the exception to ensure it can be caught by any outer exception handling
            }
        }
    }
}
