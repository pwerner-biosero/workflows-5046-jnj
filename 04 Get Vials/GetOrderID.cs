using Serilog;
using Serilog.Sinks;

using System;

using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using System.IO;


namespace Acme.Orchestrator.Scripting
{
    // Scripts require a class with a parameterless constructor and a RunAsync method matching the below signature.
    public class GetOrderID
    {
    
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
             try
            {
                await context.UpdateGlobalVariableAsync("ORDER_ID", context.OrderIdentifier);
            }
            catch (Exception ex)
            {
                 log.Error("An error occurred: " + ex.Message);
            }
        
        }
    }
}
