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
    public class GetRearrayContainer
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            string rearray_containers = context.GetGlobalVariableValue<string>("REARRAY_CONTAINERS");
            
            
                 
            var idx = context.GetGlobalVariableValue<int>("LOOP_COUNTER");
            
            var rearray_container_worklist = MetaDataProcessor.GetRearrayContainer(rearray_containers, idx);
            
            await context.UpdateGlobalVariableAsync("WORKLIST", rearray_container_worklist);
    

          
        }
    }
}

