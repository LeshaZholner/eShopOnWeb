using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderItemsReserverFunctionApp.BlobStorageService;
using OrderItemsReserverFunctionApp.Models;

namespace OrderItemsReserverFunctionApp;

public class OrderItemsReserverFunction
{
    private readonly IBlobStorageService _blobStorageService;
    public OrderItemsReserverFunction(IBlobStorageService blobStorageService)
    {
        _blobStorageService = blobStorageService;
    }

    [FunctionName("OrderItemsReserverFunction")]
    public async Task Run([ServiceBusTrigger("%QueueName%", Connection = "ServiceBusConnectionString")] ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions,
        ILogger log)
    {
        log.LogInformation($"ServiceBus queue trigger function processed message: {message.MessageId}");
        try
        {
            var order = message.Body.ToObjectFromJson<Order>();
            var data = message.Body.ToArray();

            await _blobStorageService.UploadFile($"{order.Id}.json", data);
            
            log.LogInformation("The order was upload to Blob Storage");
        }
        catch (Exception ex)
        {
            await messageActions.DeadLetterMessageAsync(message);
            log.LogError(ex, "Error uploading order to Blob Storage");
        }
    }
}
