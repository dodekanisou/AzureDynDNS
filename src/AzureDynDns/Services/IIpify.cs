using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    public interface IIpify
    {
        /// <summary>
        /// Retrieves public IP as reported by the ipify service.
        /// </summary>
        /// <returns>The public IP.</returns>
        Task<string> GetPublicIP();
    }
}
