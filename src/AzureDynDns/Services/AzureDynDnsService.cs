using AzureDynDns.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    public class AzureDynDnsService : IAzureDynDnsService
    {
        private readonly AzureDynDnsConfiguration config;
        private readonly IIpify ipifyService;
        private readonly IAzureDnsService azureDnsService;

        public AzureDynDnsService(
            AzureDynDnsConfiguration config,
            IIpify ipifyService,
            IAzureDnsService azureDnsService)
        {
            this.config = config;
            this.ipifyService = ipifyService;
            this.azureDnsService = azureDnsService;
        }

        public async Task<string> UpdateDynamicDnsRecord()
        {
            var theIp = await ipifyService.GetPublicIP();
            theIp = await azureDnsService.UpdateARecord(config.ARecordName, theIp, config.ARecordTTL);
            return theIp;
        }
    }
}
