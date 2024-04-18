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
            var reverse_rearray_picklist = MetaDataProcessor.GetReverseRearrayPicklist(context.GetGlobalVariableValue<string>("PICKLISTS"), context.GetGlobalVariableValue<int>("LOOP_COUNTER"));
            
            await context.UpdateGlobalVariableAsync("picklist", reverse_rearray_picklist);
    
        }
    }
}

