using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace OrderItemsReserverFunctionApp.BlobStorageService;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobStorageOptions _options;

    public BlobStorageService(IOptions<BlobStorageOptions> options)
    {
        _options = options.Value;
    }

    public async Task UploadFile(string fileName, byte[] data)
    {
        var blobServiceClient = new BlobServiceClient(_options.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_options.ContainerName);
        
        using (MemoryStream ms = new MemoryStream(data))
        {
            await containerClient.UploadBlobAsync(fileName, ms);
        }
    }
}
