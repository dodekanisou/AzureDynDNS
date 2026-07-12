using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    /// <summary>
    /// A service that handles DNS record modifications.
    /// </summary>
    public interface IDnsService
    {
        /// <summary>
        /// Creates or updates A records to point to the specific IP v4.
        /// </summary>
        /// <param name="aRecordNames">The records to update.</param>
        /// <param name="newIp">The IPv4 to set to.</param>
        /// <param name="aRecordTTL">Clients refresh interval in seconds, normally 1 min.</param>
        /// <returns>The number of records updated successfully.</returns>
        Task<int> UpdateARecords(IEnumerable<string> aRecordNames, string newIp, int aRecordTTL = 60);
    }
}
