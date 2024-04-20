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
    public class GetQPCRContainer 
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public  async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {


        // THe dude is abliaged
		var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
		
		(string containerJson, int number_of_containers) =MetaDataProcessor.GetReturnContainers(container_worklist, "3");
		
		await context.UpdateGlobalVariableAsync("QPCR_CONTAINER", containerJson);
		
		
		(string plate_id, string container) =MetaDataProcessor.GetReturnContainer(containerJson, 0);
		
		log.Information($"BARCODE: {plate_id}");
		
		
		await context.UpdateGlobalVariableAsync("BARCODE", plate_id);
		await context.UpdateGlobalVariableAsync("QPCR_CONTAINER", container);
		
        }

    }
    

}
