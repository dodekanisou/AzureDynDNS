using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    /// <summary>
    /// An Azure DNS client wrapper to expose management operations.
    /// </summary>
    public interface IAzureDnsService
    {
        /// <summary>
        /// Creates or updates an A record to point to the specific IP v4.
        /// </summary>
        /// <param name="aRecordName">The record to update.</param>
        /// <param name="newIp">The IPv4 to set to.</param>
        /// <param name="aRecordTTL">Clients refresh interval in seconds, normally 1 min.</param>
        /// <returns>The assigned IP to the A record.</returns>
        Task<string> UpdateARecord(string aRecordName, string newIp, int aRecordTTL = 60);
    }
}
