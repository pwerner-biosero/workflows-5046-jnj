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
    public class UpdateContainerWorklist
    {
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
            string worklist = context.GetGlobalVariableValue<string>("WORKLIST");
            
            string input_container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
            
            var updated_worklist = MetaDataProcessor.UpdateJSONWorklistWithRearrayRackBarcodes(input_container_worklist, worklist);
            
            await context.UpdateGlobalVariableAsync("Input.CONTAINER_WORKLIST", updated_worklist);
            
            await context.UpdateGlobalVariableAsync("Output.CONTAINER_WORKLIST", updated_worklist);

           // return Task.CompletedTask;
        }
    }
    
    
    public class RearrayPickList
    {
        public string AnchorBarcode {get; set;}
        public string SourceBarcode {get;set;}
        public string ContainerType {get; set;}
        public string PROCESS_LABWARE {get; set;}
        public string WorkList {get;set;}

    }
}

