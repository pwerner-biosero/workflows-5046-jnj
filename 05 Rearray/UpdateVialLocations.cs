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

        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
        	    	var worklist = context.GetGlobalVariableValue<string>("WORKLIST");
        	     
 	    	MetaDataProcessor.UpdateVialLocations(worklist);

                return Task.CompletedTask;
        }
    }
}

