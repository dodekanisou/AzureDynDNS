using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDynDns.Models
{
    public class AzureDynDnsConfiguration
    {
        public string ARecordName { get; set; }

        public int ARecordTTL { get; set; }
    }
}
