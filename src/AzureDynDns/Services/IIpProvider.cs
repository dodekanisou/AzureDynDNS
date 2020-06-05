using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    public interface IIpProvider
    {
        /// <summary>
        /// Retrieves the IP of the machine.
        /// </summary>
        /// <returns>An IP address.</returns>
        Task<string> GetIP();
    }
}
