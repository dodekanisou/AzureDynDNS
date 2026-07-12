using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDynDns.Services.DynDns
{
    public class DynDnsConfiguration
    {
        public string ARecordName { get; set; }

        public string[] ARecordNames { get; set; }

        public int ARecordTTL { get; set; }

        public IReadOnlyList<string> GetARecordNames()
        {
            var names = new List<string>();

            if (ARecordNames != null && ARecordNames.Length > 0)
            {
                names.AddRange(ARecordNames.Where(name => !string.IsNullOrWhiteSpace(name)));
            }

            if (!string.IsNullOrWhiteSpace(ARecordName))
            {
                if (!names.Contains(ARecordName, StringComparer.OrdinalIgnoreCase))
                {
                    names.Add(ARecordName);
                }
            }

            return names;
        }
    }
}
