using System.Net.Http;

namespace CustardApi.Objects;

internal class InternalHttpClientFactory : IHttpClientFactory
{
    private readonly bool _ssl;

    public InternalHttpClientFactory(bool ssl)
    {
        _ssl = ssl;
    }

    public HttpClient CreateClient(string name)
    {
        var handler = new HttpClientHandler();

        if (!_ssl)
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

        return new HttpClient(handler);
    }
}
