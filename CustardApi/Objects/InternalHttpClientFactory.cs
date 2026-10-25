using System.Net.Http;

namespace CustardApi.Objects;

internal class InternalHttpClientFactory : IHttpClientFactory
{
    private readonly bool _ssl;
    public HttpClientHandler _customHandler;

    public InternalHttpClientFactory(bool ssl, HttpClientHandler customHandler)
    {
        _ssl = ssl;
        _customHandler = customHandler;
    }

    public HttpClient CreateClient(string name)
    {
        var handler = _customHandler ?? new HttpClientHandler();

        if (!_ssl)
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;

        return new HttpClient(handler);
    }
}
