using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Dns;
using Microsoft.Azure.Management.Dns.Models;
using Microsoft.Rest.Azure.Authentication;

namespace AzureDynDns.Services.AzureDns
{
    /// <summary>
    /// An Azure DNS client wrapper to expose management operations.
    /// </summary>
    public class AzureDnsService : IDnsService
    {
        private readonly AzureDnsConfiguration config;

        public AzureDnsService(AzureDnsConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Creates or updates an A record to point to the specific IP v4.
        /// </summary>
        /// <param name="aRecordName">The record to update.</param>
        /// <param name="newIp">The IPv4 to set to.</param>
        /// <param name="aRecordTTL">Clients refresh interval in seconds, normally 1 min.</param>
        /// <returns>The assigned IP to the A record.</returns>
        public async Task<string> UpdateARecord(string aRecordName, string newIp, int aRecordTTL = 60)
        {
            // if TTL zero or less, set to default 60sec.
            if (aRecordTTL <= 0)
            {
                aRecordTTL = 60;
            }

            // https://docs.microsoft.com/en-us/azure/dns/dns-sdk
            var serviceCreds = await ApplicationTokenProvider.LoginSilentAsync(config.TenantId,
                config.ClientId, config.ClientSecret).ConfigureAwait(false);
            using (var dnsClient = new DnsManagementClient(serviceCreds) { SubscriptionId = config.SubscriptionId })
            {
                // Create record set parameters
                var recordSetParams = new RecordSet
                {
                    TTL = aRecordTTL,
                };

                // Add records to the record set parameter object.
                // In this case, we'll add a record of type 'A'
                recordSetParams.ARecords = new List<ARecord>()
                {
                        new ARecord(newIp),
                };

                // Add metadata to the record set.
                // Similar to Azure Resource Manager tags, this is optional and you can
                // add multiple metadata name/value pairs
                recordSetParams.Metadata = new Dictionary<string, string>
                {
                    { "last-update", DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture) },
                };

                // Create the actual record set in Azure DNS
                // Note: no ETAG checks specified, will overwrite existing record set if one exists
                var recordSet = await dnsClient.RecordSets.CreateOrUpdateAsync(config.ResourceGroupName,
                    config.DnsZoneName, aRecordName, RecordType.A, recordSetParams).ConfigureAwait(false);

                return recordSet.ARecords.First().Ipv4Address;
            }
        }
    }
}
