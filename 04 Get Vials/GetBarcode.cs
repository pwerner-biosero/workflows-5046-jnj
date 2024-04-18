
#r CalLabIdentities.dll
#r WorkflowHelper.dll
using System;
using System.Linq;
using Newtonsoft.Json;

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
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {

        		try
        		{
        
            		var meta_data = context.GetGlobalVariableValue<string>("PICKED_RACK_META_DATA");
            
            		StorageContainer container = JsonConvert.DeserializeObject<StorageContainer>(meta_data);
            		
            		log.Information("UPDATING RACK BARCODE");
            		
            		
            		context.UpdateGlobalVariableAsync("BARCODES", container.TUBES[0].RACK_BARCODE);
       			
       			log.Information($"RACK BARCODE:{ container.TUBES[0].RACK_BARCODE}");
            		
        		}
        		catch (ArgumentOutOfRangeException ex)
        		{
            		log.Information(ex.Message);
        		}
        	    
            		return Task.CompletedTask;
        		}

    	}
}
