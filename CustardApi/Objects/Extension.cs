using Microsoft.Extensions.DependencyInjection;
using System;

namespace CustardApi.Objects;

public static class Extension
{
    public static IServiceCollection AddCustard(this IServiceCollection services, string baseUrl)
    {
        services.AddHttpClient("custard", client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddSingleton<Service>();

        return services;
    }
}
