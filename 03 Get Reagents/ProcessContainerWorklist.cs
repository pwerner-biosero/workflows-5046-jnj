#r CalLabIdentities.dll
#r WorkflowHelper.dll
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Biosero.DataServices.Client;
using Biosero.Orchestrator.WorkflowService;



using Newtonsoft.Json;

using Serilog;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;
using CalLabUtilities;

namespace Acme.Orchestrator.Scripting
{
    public class ProcessContainerWorklist
    {
    	private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    	
        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        
            var container_worklist = context.GetGlobalVariableValue<string>("Input.CONTAINER_WORKLIST");
            
           

            var storage = context.GetGlobalVariableValue<string>("Input.STORAGE");
	
            
            //var stepNumber =(int) context.GetGlobalVariableValue<int>("Input.StepNumber");
           
           // var meta_data = CSVParser.ParseCSV(csvData, stepNumber);
        
            container_worklist = MetaDataProcessor.FilterJsonByStorageAndStep(container_worklist, storage, false, 1);
            
 	    log.Information(container_worklist);
 	    
            var number_of_racks = MetaDataProcessor.GetContainerCount(container_worklist);
            
            
            await context.UpdateGlobalVariableAsync("Input.CONTAINER_WORKLIST", container_worklist);
            
            await context.UpdateGlobalVariableAsync("NUMBER_OF_CONTAINERS",number_of_racks );
            
            log.Information($"Number of Racks {number_of_racks}");

            
           // return Task.CompletedTask;
            
     	}     
    }
}
