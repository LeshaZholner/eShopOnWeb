using System.Threading.Tasks;

namespace OrderItemsReserverFunctionApp.BlobStorageService;

public interface IBlobStorageService
{
    Task UploadFile(string fileName, byte[] data);
}
