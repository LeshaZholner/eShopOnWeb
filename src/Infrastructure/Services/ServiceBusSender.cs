using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Options;
using Microsoft.Extensions.Options;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class ServiceBusSender : IServiceBusSender
{
    private readonly ServiceBusOptions _options;

    public ServiceBusSender(IOptions<ServiceBusOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var client = new ServiceBusClient(_options.ConnectionString);
        var sender = client.CreateSender(_options.QueueName);

        await sender.SendMessageAsync(new ServiceBusMessage(message.ToJson()));
    }
}
