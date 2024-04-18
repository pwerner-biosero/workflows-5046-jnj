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
    public class UpdateVialLocations
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {



            string rearray_containers = context.GetGlobalVariableValue<string>("REARRAY_CONTAINERS");

            var idx = context.GetGlobalVariableValue<int>("LOOP_COUNTER");

            var rearray_container_worklist = MetaDataProcessor.GetReverseRearrayContainer(rearray_containers, idx);
            
            List<RearrayPickList> rearray_picklists = JsonConvert.DeserializeObject<List<RearrayPickList>>(rearray_container_worklist);


            var worklist = context.GetGlobalVariableValue<string>("WORKLIST");

            //----This Should be replaced by whatever barcode the GBG Process Grabs

            var random = new Random();

            foreach (var container in rearray_picklists)
            {
                var randomNumber = random.Next(10000, 99999);
                var barcode = $"reverse_{randomNumber}";

                container.AnchorBarcode = barcode;
            }

            var list_to_send_back =context.GetGlobalVariableValue<string>("LIST_TO_SEND_BACK");


            // Initialize as an empty JSON array if list_to_send_back is null or empty
            if (string.IsNullOrEmpty(list_to_send_back))
            {
                list_to_send_back = "[]";
            }


            List<List<RearrayPickList>> updated_rearray_picklists = JsonConvert.DeserializeObject<List<List<RearrayPickList>>>(list_to_send_back);

            updated_rearray_picklists.Add(rearray_picklists);

            var updated_stuff = JsonConvert.SerializeObject(updated_rearray_picklists);

            await context.UpdateGlobalVariableAsync("LIST_TO_SEND_BACK", updated_stuff);
            
            //var updated_rearray_container = MetaDataProcessor.UpdateReverseRearrayContainers(rearray_containers, rearray_container_worklist, idx);


            //await context.UpdateGlobalVariableAsync("REARRAY_CONTAINERS", updated_rearray_container);

            //--Need to check this out because it should be more than one barcode to update
            //worklist = MetaDataProcessor.UpdateVialLocations(worklist, barcode);

            await context.UpdateGlobalVariableAsync("WORKLIST", worklist);


        }
    }
}

