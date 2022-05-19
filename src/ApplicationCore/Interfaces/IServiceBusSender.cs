using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IServiceBusSender
{
    Task SendMessageAsync<T>(T message);
}
