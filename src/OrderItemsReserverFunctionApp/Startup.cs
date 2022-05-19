using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderItemsReserverFunctionApp.BlobStorageService;

[assembly: FunctionsStartup(typeof(OrderItemsReserverFunctionApp.Startup))]
namespace OrderItemsReserverFunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddOptions<BlobStorageOptions>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection(nameof(BlobStorageOptions)).Bind(settings);
               });
        builder.Services.AddTransient<IBlobStorageService, BlobStorageService.BlobStorageService>();
    }
}
