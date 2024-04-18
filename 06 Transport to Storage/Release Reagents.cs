
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
    public class Release_Reagents 
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public   Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {

		
		var container =  context.GetGlobalVariableValue<string>("META_DATA");
		
		MetaDataProcessor.ReleaseReagents(container);
	
     		 return Task.CompletedTask;

        }

    }
    

}
