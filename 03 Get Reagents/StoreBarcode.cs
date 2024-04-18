
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
    public class StoreBarcode
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
       
           var barcode =  context.GetGlobalVariableValue<string>("BARCODE");
            
            var barcode_list  = context.GetGlobalVariableValue<string>("BARCODE_LIST");  
            log.Information($"Barcode List: {barcode_list}");
            
           
        	 
        	    barcode_list =  BarcodeManager.AddBarcodeToList(barcode_list, barcode);
        	    
        	    log.Information($"Barcode List: {barcode_list}");
        	    
        	    context.UpdateGlobalVariableAsync("BARCODE_LIST", barcode_list);

            return Task.CompletedTask;
        }
        
          // Dummy implementation of random barcode generation
        private string GenerateRandomBarcode()
        {
            // You can replace this with your actual barcode generation logic
            Random random = new Random();
            return "BC" + random.Next(1000, 9999).ToString();
        }

    }
}
