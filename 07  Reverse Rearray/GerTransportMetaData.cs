

#r CalLabIdentities.dll
#r Inventory_Helper.dll
#r CredentialManagement.dll
#r Microsoft.Data.SqlClient.dll
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

namespace Acme.Orchestrator.Scripting
{
    public class GerTransportMetaData
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            string rearray_containers = context.GetGlobalVariableValue<string>("REARRAY_CONTAINERS");
            
            
             string stuff_to_Check = context.GetGlobalVariableValue<string>("LIST_TO_SEND_BACK");
            MetaDataProcessor.CheckListToSendBack(stuff_to_Check);
                 
            var idx = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            idx = idx-1;
            
            
            /*
            
            var rearray_container_worklist = MetaDataProcessor.GetReverseRearrayContainer(rearray_containers, idx);
            
            var rearray_picklist = JsonConvert.DeserializeObject<List<RearrayPickList>>(rearray_container_worklist);
            
            var plate_id = rearray_picklist[0].AnchorBarcode;
            

            
            Console.WriteLine($"[GENERATING TRANSPORT META DATA - {plate_id}]");
            
            //Console.WriteLine(rearray_container_worklist);
            
             string json = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
          
             var transport_meta_data = MetaDataProcessor.GetRearrayTransportMetaData(plate_id, json);
             
             var storage_location = MetaDataProcessor.GetReuseStorageLocation(transport_meta_data);
              
   
              
              await context.UpdateGlobalVariableAsync("TRANSPORT_METADATA",transport_meta_data );
              
              await context.UpdateGlobalVariableAsync("TRANSPORT_BARCODE", plate_id);
              
              */
            
           
              //return Task.CompletedTask;
        }
    }
    
    

    
}

