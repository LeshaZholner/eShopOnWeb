using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Options;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Mime;

namespace Microsoft.eShopWeb.Infrastructure.Services;

public class ApiClient : IApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApiOptions _options;

    public ApiClient(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> options, ILogger<ApiClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task PostAsync<T>(T body)
    {
        var client = _httpClientFactory.CreateClient();
        var bodyData = new StringContent(body.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await client.PostAsync(_options.Url, bodyData);
    }
}
