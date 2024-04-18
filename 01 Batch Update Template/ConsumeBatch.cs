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
using CalLabUtilities;
using Serilog.Sinks;
using Serilog.Sinks.SystemConsole.Themes;
using Inventory_Helper;
using Biosero.DataModels.Clients;
using Biosero.DataModels.Events;
using Serilog.Core;
using Biosero.DataServices;


using System.Dynamic;



namespace Acme.Orchestrator.Scripting
{
   
    public class ConsumeBatch 
    {
        //--This can't be localhost in docker

        private static Configuration config = ConfigLoader.LoadConfiguration("cal_config.json");
        private static string url = config.AppSettings.DataServicesURL;
        private static string db_connection_string = config.AppSettings.DatabaseConectionString;
        private static AccessioningClient accessioningClient = new AccessioningClient(url);
        private static QueryClient queryClient = new QueryClient(url);
        private static ILogger log = new LoggerConfiguration().WriteTo.Console().CreateLogger();

        public async Task RunAsync(DataServicesClient client, WorkflowContext context, CancellationToken cancellationToken)
        {
        try
            {

                // Retrieve global variables
                var global_settings = queryClient.GetIdentity("GLOBAL_SETTINGS").Properties;

                //--Deterimines which DNA Workcells are in operation and assigns to a workflow variable
                var dna_worcell_settinggs = global_settings.FirstOrDefault(p => p.Name == "DNA_WORKCELL").Value;
                await context.UpdateGlobalVariableAsync("DNA_WORKCELL_SETTINGS", dna_worcell_settinggs);
                
                //--Deterimines which Cold Storage Units are in operation and assigns to a workflow variable
                var cold_store_settings = global_settings.FirstOrDefault(p => p.Name == "COLD_STORAGE").Value;
                await context.UpdateGlobalVariableAsync("COLD_STORE_SETTINGS", cold_store_settings);

                //--Process the Batch JSON which includes all of the Sample Meta Data and Analytical Methods to run.
                var jsonString = context.GetGlobalVariableValue<string>("Input.BatchJSON");
                BatchOrder batchOrder = JsonConvert.DeserializeObject<BatchOrder>(jsonString);


                var method = context.GetGlobalVariableValue<string>("Input.method");
                
                // Sort the samples in the batch order into separate lists based on their sample type ("Unknown", "Standard", "Blank")
                var samples = batchOrder.Samples.Where(s => s.SampleType == "Unknown").ToList();
                var standards = batchOrder.Samples.Where(s => s.SampleType == "Standard").ToList();
                var blanks = batchOrder.Samples.Where(s => s.SampleType == "Blank").ToList();

                // Get the number of UNKNOWN samples in the batch order
                int numberOfSamples = samples.Count();

                var batchServiceOrderID = batchOrder.Id;

	
                // Generate liquid handling worklist based on Samples Proivded in the Batch Order
                // TODO Add stage
		 var liquid_handling_worklist = WorkflowHelper.GenerateLiquidHandlingWorklists(batchOrder);


                // The Analytical Method ID is the Identifier of the DataServices Identity that represents the Analytical Method                
                var protocol = queryClient.GetIdentity( batchOrder.Tasks[0].DocumentId);

                // Get the solutions and steps for the protocol
                var solutions = queryClient.GetChildIdentities(protocol.Identifier, 100, 0);

                // Split the solutions and steps (Steps are also child identities of the protocol)
                var result = WorkflowHelper.SplitSolutionsAndSteps(solutions);
                
                var sample_identity_metadata = solutions.FirstOrDefault(s => s.Name == "Sample");
                var standard_identity_metadata = solutions.FirstOrDefault(s => s.Name == "Standard");

                // This writes all of the Solutions and Reagent information to the DS Console
                WorkflowHelper.LogInputInformation(method, numberOfSamples);

                // Get the Steps of the workflow - Steps control when containers will be sent to the DNA workcells and removed from the DNA Workcells
                var steps = result.Steps;
                var stepList = WorkflowHelper.ProcessSteps(steps);

                // Process the solutions and calculate the volumes of each solution and reagent needed for the batch order and assign to a workflow variable
                var allSolutionData = WorkflowHelper.ProcessSolutionsAndCalculateVolumes(result.Solutions, numberOfSamples);
                await context.UpdateGlobalVariableAsync("ALL_SOLUTION_DATA", JsonConvert.SerializeObject(allSolutionData));
                
                WorkflowHelper.PrintAllSolutionsAndReagents(allSolutionData);

                // Select vials and solutions that meet the requirements of the batch order
                var selectedVials = WorkflowHelper.SelectVialsAndSolutions(allSolutionData, batchServiceOrderID, out var missingIdentities);

                // Assign Protocol Specific Metadata to the Samples and Standards in the Batch Order
                WorkflowHelper.ProcessSamples(samples, sample_identity_metadata, batchServiceOrderID, ref selectedVials, ref missingIdentities);
                WorkflowHelper.ProcessSamples(standards, standard_identity_metadata, batchServiceOrderID, ref selectedVials, ref missingIdentities);

                // TODO REMOVE THIS LINE AFTER TESTING
                //missingIdentities.Clear();
                
                // Thow an error in the Workflow if any of the samples, standards or reagents are missing
                WorkflowHelper.HandleMissingIdentities(missingIdentities);

                // Reserve the reagents for the batch order so that they are not used by other batch orders
                WorkflowHelper.ReserveReagents(selectedVials, batchServiceOrderID);

                // Generate the reagent and sample picklists for all storage devices
                var (reagentPickList, jsonPickList) = WorkflowHelper.GenerateReagentPickLists(selectedVials, stepList, numberOfSamples, allSolutionData);
                
                await context.UpdateGlobalVariableAsync("CONTAINER_JSON", jsonPickList );
              
            }
            catch (Exception ex)
            {
                var message = $"{ex.GetType().Name}: {ex.Message}";
                log.Error(message);
                await context.UpdateGlobalVariableAsync("ErrorMessage", message);
            }



        }

    }
    

    }
