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
    public class RTReagentGetBarcode
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
        		var barcode_list  = context.GetGlobalVariableValue<string>("RT_REAGENT_BARCODE_LIST");  
        		var index = context.GetGlobalVariableValue<int>("RT_REAGENT_LOOP_COUNTER");  
        		
        		var index_adjustment = 0;//context.GetGlobalVariableValue<int>("AZENTA_LOOP_ADJUSTMENT");  
        		
        		index = index - index_adjustment;

                log.Information($"INDEX:{index.ToString()}");
        		
        		try
        		{
        	
           		 string barcodeAtIndex = BarcodeManager.GetBarcodeByIndex(barcode_list, index);
           		 log.Information($"BARCODE:{barcodeAtIndex}");
           		 context.UpdateGlobalVariableAsync("RT_REAGENT_BARCODE", barcodeAtIndex);
           		 
           		 
            		log.Information("Barcode at " + barcodeAtIndex); // Output: BC3
        		}
        		catch (ArgumentOutOfRangeException ex)
        		{
            		log.Information(ex.Message);
        		}
        	    
            		return Task.CompletedTask;
        		}

    	}
}
