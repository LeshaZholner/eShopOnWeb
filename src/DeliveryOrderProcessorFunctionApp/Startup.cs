using System;
using DeliveryOrderProcessorFunctionApp.CosmosDbService;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(DeliveryOrderProcessorFunctionApp.Startup))]
namespace DeliveryOrderProcessorFunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddOptions<CosmosDbOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(CosmosDbOptions)).Bind(settings);
            });
        builder.Services.AddTransient<ICosmosDbService, CosmosDbService.CosmosDbService>();
    }
}
