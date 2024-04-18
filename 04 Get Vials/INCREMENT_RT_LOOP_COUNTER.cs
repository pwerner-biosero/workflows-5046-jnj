using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;

namespace Acme.Orchestrator.Scripting
{
    // Scripts require a class with a parameterless constructor and a RunAsync method matching the below signature.
    public class INIT_RT_LOOP_COUNTER
    {
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
       	     var counter = context.GetGlobalVariableValue<int>("RT_LOOP_COUNTER") + 1;

       	    context.UpdateGlobalVariableAsync("RT_LOOP_COUNTER", counter);
       	
            return Task.CompletedTask;
        }
    }
}