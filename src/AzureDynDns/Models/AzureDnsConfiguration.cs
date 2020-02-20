using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AzureDynDns.Models
{
    public class AzureDnsConfiguration
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string TenantId { get; set; }

        public string SubscriptionId { get; set; }

        public string ResourceGroupName { get; set; }

        public string DnsZoneName { get; set; }
    }
}
