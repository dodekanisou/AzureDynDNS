using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureDynDns.Services;
using AzureDynDns.Services.Ipify;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AzureDynDNS.Tests.Unit
{
    public class IpifyTests
    {
        private const string IP_TO_CHECK = "10.0.0.1";
        private readonly HttpClient failedAttempt;
        private readonly HttpClient successfulAttempt;
        private readonly ILogger<IIpProvider> logger;
        private readonly IpifyConfiguration config;

        public IpifyTests()
        {
            // Based on
            // http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/
            failedAttempt =
                new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                }));

            successfulAttempt =
                new HttpClient(new FakeHttpMessageHandler(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(IP_TO_CHECK, Encoding.UTF8,
                                                "text/plain")
                }));

            logger = new Mock<ILogger<IIpProvider>>().Object;
            config = new IpifyConfiguration();
        }

        [Fact]
        public async Task
        Returns_the_ip_address()
        {
            // Prepare - The service
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(i => i.CreateClient(It.IsAny<string>()))
                .Returns(successfulAttempt);
            var service = new IpifyService(config, httpClientFactory.Object, logger);

            // Act - Get the IP
            var ip = await service.GetIP();

            // Assert
            Assert.Equal(IP_TO_CHECK, ip);
        }

        [Fact]
        public async Task
        Returns_null_if_service_fails()
        {
            // Prepare - The service
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(i => i.CreateClient(It.IsAny<string>()))
                .Returns(failedAttempt);
            var service = new IpifyService(config, httpClientFactory.Object, logger);

            // Act - Get the IP
            var ip = await service.GetIP();

            // Assert
            Assert.Null(ip);
        }
    }
}
