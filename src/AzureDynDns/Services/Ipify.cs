using AzureDynDns.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
public class Ipify : IIpify
{
    private readonly IHttpClientFactory clientFactory;
    private readonly ILogger<IIpify> logger;
    private readonly Uri serviceUri;

    public Ipify(IpifyConfiguration config, IHttpClientFactory httpFactory,
                 ILogger<IIpify> logger)
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

    public async Task<string> GetPublicIP()
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
