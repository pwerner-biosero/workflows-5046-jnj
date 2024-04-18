
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
    public class GetNumberOfRacks
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {    

		
			var number_of_racks = MetaDataProcessor.GetNumberOfReverseRearrayContainers(context.GetGlobalVariableValue<string>("Input.PICKLIST"));
			
			await context.UpdateGlobalVariableAsync("NUMBER_OF_RACKS", number_of_racks);
			
       }
   }
 
}