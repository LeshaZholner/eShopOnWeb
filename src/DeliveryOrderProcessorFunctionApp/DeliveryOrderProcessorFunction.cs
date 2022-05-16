using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DeliveryOrderProcessorFunctionApp.CosmosDbService;
using System;
using DeliveryOrderProcessorFunctionApp.Models;

namespace DeliveryOrderProcessorFunctionApp;

public class DeliveryOrderProcessorFunction
{
    private readonly ICosmosDbService _cosmosDb;

    public DeliveryOrderProcessorFunction(ICosmosDbService cosmosDb)
    {
        _cosmosDb = cosmosDb;
    }

    [FunctionName("DeliveryOrderProcessorFunction")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {

        log.LogInformation("HTTP trigger function processed a request.");

        try
        {
            var json = await req.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(json);
            await _cosmosDb.AddAsync(order);

            log.LogInformation("The order added to CosmosDb");
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Error adding order to CosmosDb");
            return new ObjectResult(new { error = ex.Message }) { StatusCode = 500 };
        }

        return new OkObjectResult(new { success = true });
    }
}
