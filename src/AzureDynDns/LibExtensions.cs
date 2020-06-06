using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using AzureDynDns.Services;
using AzureDynDns.Services.AzureDns;
using AzureDynDns.Services.DynDns;
using AzureDynDns.Services.IpFromArguments;
using AzureDynDns.Services.Ipify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDynDns {
public static class LibExtensions {
    public static IServiceCollection
    RegisterServices(this IServiceCollection services,
                     IConfiguration configuration, string[] args) {
        // Register the default IHttpClientFactory
        services.AddHttpClient();

        // Register the AzureDns service
        AzureDnsConfiguration azureDnsConfig = new AzureDnsConfiguration();
        configuration.Bind("Settings", azureDnsConfig);
        services.AddSingleton(azureDnsConfig);
        services.AddSingleton<IDnsService, AzureDnsService>();

        // Register the IP provider
        switch (configuration["IPSource"]) {
        case "arguments":
            // Register the IpFromArguments service
            IpFromArgumentsConfiguration ipFromArgumentsConfig =
                new IpFromArgumentsConfiguration();
            configuration.Bind("Settings", ipFromArgumentsConfig);
            ipFromArgumentsConfig.Arguments = args;
            services.AddSingleton(ipFromArgumentsConfig);
            services.AddSingleton<IIpProvider, IpFromArgumentsService>();
            break;
        case "ipify":
        default:
            // Register the Ipify service
            IpifyConfiguration ipifyConfig = new IpifyConfiguration();
            configuration.Bind("Settings", ipifyConfig);
            services.AddSingleton(ipifyConfig);
            services.AddSingleton<IIpProvider, IpifyService>();
            break;
        }

        // Register the DynDns main application
        DynDnsConfiguration azureDynDnsConfig = new DynDnsConfiguration();
        configuration.Bind("Settings", azureDynDnsConfig);
        services.AddSingleton(azureDynDnsConfig);
        services.AddSingleton<IDynDnsService, DynDnsService>();

        return services;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
         "Design", "CA1031:Do not catch general exception types",
         Justification = "Unsure of what exceptions may be thrown at this point")]
    public static async Task<string>
    SafeReadStringContentsAsync(this HttpResponseMessage responseMessage) {
        string content = string.Empty;
        if (responseMessage == null) {
            return content;
        }

        try {
            content =
                await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(
                    false);
        } catch (Exception) {
            // Ignore failure to read response
        }

        return content;
    }

    public static string AssemblyDirectory(this Assembly assembly) {
        string codeBase = assembly.CodeBase;
        UriBuilder uri = new UriBuilder(codeBase);
        string path = Uri.UnescapeDataString(uri.Path);
        return System.IO.Path.GetDirectoryName(path);
    }
}
}
