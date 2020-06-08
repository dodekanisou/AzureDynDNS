using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace AzureDynDns.Services.Ipify
{
    /// <summary>
    /// Provides the public IP of the service through the free ipify service.
    /// </summary>
    public class IpifyService : IIpProvider
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<IIpProvider> logger;
        private readonly Uri serviceUri;

        public IpifyService(IpifyConfiguration config, IHttpClientFactory httpFactory,
                     ILogger<IIpProvider> logger)
        {
            // Use default public service if not specified otherwise
            if (string.IsNullOrWhiteSpace(config.IpifyServiceAddress))
            {
                config.IpifyServiceAddress = "https://api.ipify.org";
            }

            serviceUri = new Uri(config.IpifyServiceAddress);
            clientFactory = httpFactory;
            this.logger = logger;
        }

        public async Task<string> GetIP()
        {
            using (var client = clientFactory.CreateClient())
            {
                using (HttpResponseMessage response =
                          await client.GetAsync(serviceUri).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var ip = await response.Content.ReadAsStringAsync().ConfigureAwait(
                            false);
                        logger.LogInformation("Retrieved public IP {publicIP}", ip);
                        return ip;
                    }
                    else
                    {
                        string errorMessage =
                            await response.SafeReadStringContentsAsync().ConfigureAwait(
                                false);
                        logger.LogError(
                            "Failed to invoke {apiUrl}. Status code {statusCode}. Error message {errorMessage}",
                            serviceUri, response.StatusCode, errorMessage);
                        return null;
                    }
                }
            }
        }
    }
}
