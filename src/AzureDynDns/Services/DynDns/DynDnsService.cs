using System.Threading.Tasks;

namespace AzureDynDns.Services.DynDns
{
    public class DynDnsService : IDynDnsService
    {
        private readonly DynDnsConfiguration config;
        private readonly IIpProvider ipifyService;
        private readonly IDnsService azureDnsService;

        public DynDnsService(
            DynDnsConfiguration config,
            IIpProvider ipifyService,
            IDnsService azureDnsService)
        {
            this.config = config;
            this.ipifyService = ipifyService;
            this.azureDnsService = azureDnsService;
        }

        public async Task<string> UpdateDynamicDnsRecord()
        {
            var theIp = await ipifyService.GetIP();
            theIp = await azureDnsService.UpdateARecord(config.ARecordName, theIp, config.ARecordTTL);
            return theIp;
        }
    }
}
