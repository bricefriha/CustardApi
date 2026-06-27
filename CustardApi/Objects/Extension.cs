using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustardApi.Objects;

public static class Extension
{
    public static IServiceCollection AddCustard(this IServiceCollection services, string baseUrl, string host, int port = 80, bool sslCertificate = false)
    {
        services.AddHttpClient("custard", client =>
        {
            var scheme = sslCertificate ? "https" : "http";
            var baseUrl = $"{scheme}://{host}{(port == 80 ? "/" : ":" + port + "/")}";
            client.BaseAddress = new Uri(baseUrl);
        });

        services.AddSingleton<Service>(sp =>
            new Service(
                host: host,
                port: port,
                sslCertificate: sslCertificate,
                handler: null // or a custom handler if you need one
            ));

        return services;
    }
}
