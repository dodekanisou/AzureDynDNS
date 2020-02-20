using AzureDynDns.Models;
using AzureDynDns.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureDynDns
{
    public static class LibExtensions
    {

        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
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
            AzureDynDnsConfiguration azureDynDnsConfig = new AzureDynDnsConfiguration();
            configuration.Bind("Settings", azureDynDnsConfig);
            services.AddSingleton(azureDynDnsConfig);

            // Register the default IHttpClientFactory
            services.AddHttpClient();

            // Register application services
            services.AddSingleton<IIpify, Ipify>();
            services.AddSingleton<IAzureDnsService, AzureDnsService>();
            services.AddSingleton<IAzureDynDnsService, AzureDynDnsService>();

            return services;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Unsure of what exceptions may be thrown at this point")]
        public static async Task<string> SafeReadStringContentsAsync(this HttpResponseMessage responseMessage)
        {
            string content = string.Empty;
            if (responseMessage == null)
            {
                return content;
            }

            try
            {
                content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                // Ignore failure to read response
            }

            return content;
        }
    }
}
