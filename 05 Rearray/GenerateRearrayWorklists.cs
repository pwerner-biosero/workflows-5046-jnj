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
    public class GenerateRearrayWorklists
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        	    
        	    string json = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
        
            var rearray_containers =  MetaDataProcessor.ProcessRearray(json);
            
            Console.WriteLine(rearray_containers);
            
            List<StorageContainer> containers = JsonConvert.DeserializeObject<List<StorageContainer>>(rearray_containers);
            
            var number_of_racks = containers.Count;
            
            await context.UpdateGlobalVariableAsync("NUMBER_OF_RACKS", number_of_racks);
            
            await context.UpdateGlobalVariableAsync("REARRAY_CONTAINERS", rearray_containers);


            
        }


    }
}
