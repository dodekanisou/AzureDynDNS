using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDynDns.Services
{
    public interface IAzureDynDnsService
    {
        Task<string> UpdateDynamicDnsRecord();
    }
}
