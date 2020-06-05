using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    /// <summary>
    /// A service to retrieve an IP and update a DNS record.
    /// </summary>
    public interface IDynDnsService
    {
        Task<string> UpdateDynamicDnsRecord();
    }
}
