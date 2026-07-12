using AzureDynDns.Services.DynDns;
using Xunit;

namespace AzureDynDNS.Tests.Unit
{
    public class DynDnsConfigurationTests
    {
        [Fact]
        public void Returns_record_names_from_array_when_present()
        {
            var config = new DynDnsConfiguration
            {
                ARecordNames = new[] { "www", "api" },
            };

            var names = config.GetARecordNames();

            Assert.Equal(new[] { "www", "api" }, names);
        }

        [Fact]
        public void Falls_back_to_single_record_name_when_array_is_empty()
        {
            var config = new DynDnsConfiguration
            {
                ARecordName = "www",
            };

            var names = config.GetARecordNames();

            Assert.Equal(new[] { "www" }, names);
        }
    }
}
