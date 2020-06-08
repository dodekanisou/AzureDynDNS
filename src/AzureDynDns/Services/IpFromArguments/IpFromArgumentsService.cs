using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureDynDns.Services.IpFromArguments.Exceptions;
using Microsoft.Extensions.Logging;

namespace AzureDynDns.Services.IpFromArguments
{
    public class IpFromArgumentsService : IIpProvider
    {
        private readonly ILogger<IIpProvider> logger;
        private IpFromArgumentsConfiguration config;

        public IpFromArgumentsService(IpFromArgumentsConfiguration config,
                                      ILogger<IIpProvider> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public Task<string> GetIP()
        {
            if (config.ArgumentIndex < 0)
            {
                throw new NegativeIndexException();
            }

            if (config.Arguments == null || !config.Arguments.Any())
            {
                throw new NoArgumentsException();
            }

            if (config.ArgumentIndex > config.Arguments.Count - 1)
            {
                throw new IndexOutOfBoundsException(config);
            }

            return Task.FromResult(config.Arguments[config.ArgumentIndex]);
        }
    }
}
