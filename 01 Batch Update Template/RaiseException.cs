using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Biosero.Workflow;


namespace Acme.Orchestrator.Scripting
{
{
    public class RaiseException : WorkflowScript
    {
       
        private static Logger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
          log.Error("We have a situation");
        }
    }
}