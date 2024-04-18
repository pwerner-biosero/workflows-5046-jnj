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




/* This Script Adds  Rack barcodes of the picked racks to the Predefined worklist  */

namespace Acme.Orchestrator.Scripting
{
    public class UpdateRearrayWorklist
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            
           	


		log.Information($"[{"RT STORE"}] Updating Worklist");

            	var storage_worklist = BuildWorklist(context, "BARCODE_LIST", "Input.STORAGE_WORKLIST");
	
            
            	context.UpdateGlobalVariableAsync("Output.STORAGE_WORKLIST", storage_worklist);
            	Console.WriteLine(storage_worklist);
            
            	return Task.CompletedTask;
           
            
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



