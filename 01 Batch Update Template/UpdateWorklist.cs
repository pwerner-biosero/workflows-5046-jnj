#r CalLabIdentities.dll
#r Inventory_Helper.dll
#r CredentialManagement.dll
#r Microsoft.Data.SqlClient.dll
#r WorkflowHelper.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;

using Biosero.DataServices.RestClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Text.Json.Nodes;
using Biosero.DataModels.Resources;
using Biosero.DataModels.Parameters;
using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;
using Inventory_Helper;
using Biosero.DataModels.Clients;
using Biosero.DataModels.Events;
using Serilog.Core;
using Biosero.DataServices;

using CalLabUtilities;
using System.Dynamic;



namespace Acme.Orchestrator.Scripting
{
    public class UpdateWorklist 
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {

              
                var storageTypeIds = new[]
                {
                    StorageTypeIDs.REARRAY_1_AMBIENT,
                    StorageTypeIDs.AZENTA_MINUS_80,
                    StorageTypeIDs.AZENTA_MINUS_20,
                    StorageTypeIDs.VERSO_Q20_4C_1,
                    StorageTypeIDs.VERSO_Q20_4C_2,
                   // StorageTypeIDs.ASKION_MINUS_180,
                    StorageTypeIDs.RTSTORE_1_AMBIENT
                };

 		var main_worklist =    context.GetGlobalVariableValue<string>("CONTAINER_JSON");
 		
 		
                foreach (var storageTypeId in storageTypeIds)
                {
                      var container_list = context.GetGlobalVariableValue<string>(storageTypeId.Name);
                      
                      main_worklist = MetaDataProcessor.UpdateJSONWorklist(main_worklist, container_list);
             
                }
                
                await context.UpdateGlobalVariableAsync("CONTAINER_JSON", main_worklist);
                
                Console.WriteLine("***************************");
                
                 Console.WriteLine(main_worklist);
                
                

        }

    }
    

}
