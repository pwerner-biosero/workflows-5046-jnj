#r CalLabIdentities.dll
#r WorkflowHelper.dll
using System;
using System.Linq;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;
using CalLabUtilities;

using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;


namespace Acme.Orchestrator.Scripting
{
    public class GetRTtoSTORAGEMetaData
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
 
            return MetaDataProcessor.ProcessDataAsync(context, 0, "Input.STORAGE_WORKLIST", "EMPTY_RACK_META_DATA", "LOOP_COUNTER" );
        }

    }
}
