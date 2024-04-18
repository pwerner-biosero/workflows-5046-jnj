#r CalLabIdentities.dll
#r WorkflowHelper.dll
#r BatchingServiceClient.dll
#r System.Windows.Forms.dll

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
//using Biosero.DataModels.Ordering;


namespace Acme.Orchestrator.Scripting
{
    public class CheckInContainers
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            try
            {
            
            	Configuration config = ConfigLoader.LoadConfiguration("cal_config.json");
            	
                var rack_barcode = context.GetGlobalVariableValue<string>("Input.RackBarcode");
                var barcodes = context.GetGlobalVariableValue<string>("Input.ZIATH");
                var receipt_station = context.GetGlobalVariableValue<string>("Input.RECEIPT_STATION");

                log.Information($"[RACK BARCODE] {rack_barcode}");
                log.Information($"[SET TRANSPORT ORIGIN] {receipt_station}");

                var bc_list = BarcodeManager.ParseTubeData(barcodes);
                var bc_json = BarcodeManager.ConvertListToJson(bc_list);

                var url = config.AppSettings..InteligentBatchingURL;
                
                IntelligentBatchingServiceClient ibsc = new IntelligentBatchingServiceClient(url);

                string json_samples = await ibsc.SampleLookup(bc_json);

                await context.UpdateGlobalVariableAsync("JSON_SAMPLE_LIST", json_samples);
            }
            catch (Exception ex)
            {
                log.Error($"An error occurred during the process: {ex.Message}");
                throw; // Re-throwing the exception to ensure it can be caught by any outer exception handling
            }
        }
    }
}
