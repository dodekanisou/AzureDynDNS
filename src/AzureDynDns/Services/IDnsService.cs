﻿using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    /// <summary>
    /// A service that handles DNS record modifications.
    /// </summary>
    public interface IDnsService
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
