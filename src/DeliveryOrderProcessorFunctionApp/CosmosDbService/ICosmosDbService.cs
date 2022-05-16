using System.Threading.Tasks;

namespace DeliveryOrderProcessorFunctionApp.CosmosDbService;

public interface ICosmosDbService
{
    Task AddAsync<T>(T item);
}
