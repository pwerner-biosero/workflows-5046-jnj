




#r CalLabIdentities.dll
#r Inventory_Helper.dll
#r CredentialManagement.dll
#r Microsoft.Data.SqlClient.dll
#r WorkflowHelper.dll
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using CalLabUtilities;
using Serilog;

namespace Acme.Orchestrator.Scripting
{
    public class GetReverseRearrayTransportMetadata
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {   
		var picklist = context.GetGlobalVariableValue<string>("Input.PICKLIST");
        		
        		var idx = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
        	
                var (barcode, storage_destination, transport_metadata) = MetaDataProcessor.GetReverseRearrayTransportMetadata(picklist, idx );
                
                await context.UpdateGlobalVariableAsync("TRANSPORT_METADATA", transport_metadata);
                
                await context.UpdateGlobalVariableAsync("BARCODE", barcode);
                
                 await context.UpdateGlobalVariableAsync("DESTINATION", storage_destination);
		
		Console.WriteLine(transport_metadata);
			
       }
   }
 
}