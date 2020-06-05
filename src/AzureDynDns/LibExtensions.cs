using System;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDynDns.Services;
using AzureDynDns.Services.AzureDns;
using AzureDynDns.Services.DynDns;
using AzureDynDns.Services.Ipify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDynDns
{
    public static class LibExtensions
    {
        public static IServiceCollection
        RegisterServices(this IServiceCollection services,
                         IConfiguration configuration)
        {
            // Register the AzureDnsConfiguration object
            AzureDnsConfiguration azureDnsConfig = new AzureDnsConfiguration();
            configuration.Bind("Settings", azureDnsConfig);
            services.AddSingleton(azureDnsConfig);

            // Register the IpifyConfiguration object
            IpifyConfiguration ipifyConfig = new IpifyConfiguration();
            configuration.Bind("Settings", ipifyConfig);
            services.AddSingleton(ipifyConfig);

            // Register the AzureDynDnsConfiguration object
            DynDnsConfiguration azureDynDnsConfig = new DynDnsConfiguration();
            configuration.Bind("Settings", azureDynDnsConfig);
            services.AddSingleton(azureDynDnsConfig);

            // Register the default IHttpClientFactory
            services.AddHttpClient();

            // Register application services
            services.AddSingleton<IIpProvider, IpifyService>();
            services.AddSingleton<IDnsService, AzureDnsService>();
            services.AddSingleton<IDynDnsService, DynDnsService>();

            return services;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", "CA1031:Do not catch general exception types",
            Justification = "Unsure of what exceptions may be thrown at this point")]
        public static async Task<string>
        SafeReadStringContentsAsync(this HttpResponseMessage responseMessage)
        {
            string content = string.Empty;
            if (responseMessage == null)
            {
                return content;
            }

            try
            {
                content =
                    await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(
                        false);
            }
            catch (Exception)
            {
                // Ignore failure to read response
            }

            return content;
        }
    }
}
