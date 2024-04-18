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
   		var (rearrayContainers, picklists, numberOfRacks) = MetaDataProcessor.ProcessReverseRearray(context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST"));

    		Console.WriteLine(rearrayContainers);
    		await context.UpdateGlobalVariableAsync("REARRAY_CONTAINERS", rearrayContainers);
    		
    		await context.UpdateGlobalVariableAsync("PICKLISTS", picklists);

    		await context.UpdateGlobalVariableAsync("NUMBER_OF_RACKS", numberOfRacks);
       }
   }
}