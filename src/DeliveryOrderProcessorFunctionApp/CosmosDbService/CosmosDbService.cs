using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace DeliveryOrderProcessorFunctionApp.CosmosDbService;

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosDbOptions _options;

    public CosmosDbService(IOptions<CosmosDbOptions> options)
    {
        _options = options.Value;
    }

    public async Task AddAsync<T>(T item)
    {
        using CosmosClient client = new CosmosClient(_options.EndpointUri, _options.PrimaryKey);
        var database = client.GetDatabase(_options.DatabaseId);
        var container = database.GetContainer(_options.ContainerId);

        await container.CreateItemAsync(item);
    }
}
