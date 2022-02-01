using AzureDynDns.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace AzureDynDns
{
    public static class Program
    {
        public static void Main()
        {
            var assembly = Assembly.GetEntryAssembly();

            // Create configuration object
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(assembly.AssemblyDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .AddUserSecrets(assembly, true)
                                        .Build();

            // Setup our DI
            var serviceProvider = new ServiceCollection()
                                      .AddLogging()
                                      .AddSingleton<IConfiguration>(config)
                                      .RegisterServices(config)
                                      .AddLogging((builder) =>
                                      {
                                          builder.AddConsole();
                                          builder.SetMinimumLevel(LogLevel.Trace);
                                      })
                                      .BuildServiceProvider();

            var updateService = serviceProvider.GetService<IDynDnsService>();
            var ip = updateService.UpdateDynamicDnsRecord().GetAwaiter().GetResult();
            Console.WriteLine($"Assigned IP {ip}");
        }
    }
}
