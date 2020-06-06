using AzureDynDns.Services;
using AzureDynDns.Services.IpFromArguments;
using AzureDynDns.Services.IpFromArguments.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AzureDynDNS.Tests.Unit {
  public class IpFromArgumentsTests {
    private static readonly string[] ArgumentsList = new string[]{
        "device name", "1500", "1531", "192.168.1.2", "255.255.255.0"};
    private readonly ILogger<IIpProvider>logger;

    public IpFromArgumentsTests() {
      logger = new Mock<ILogger<IIpProvider>>().Object;
    }

    [Theory]
    [MemberData(nameof(ArgumentsData))]
        // Test that the service returns the right value for the given index
        public async Task
        Returns_correct_argument(int index, string expected_value) {
      // Prepare - The service
      var config = new IpFromArgumentsConfiguration(){Arguments = ArgumentsList,
                                                      ArgumentIndex = index};
      var service = new IpFromArgumentsService(config, logger);

      // Act - Get the IP
      var ip = await service.GetIP();

      // Assert
      Assert.Equal(expected_value, ip);
    }

    public static IEnumerable<object[]>ArgumentsData() {
      var output = new List<object[]>();

      for (var index = 0; index < ArgumentsList.Length; index++) {
        output.Add(new object[]{index, ArgumentsList[index]});
      }

      return output;
    }

    [Fact]
    public async Task
    Throws_exceptions_if_no_arguments_passedAsync() {
      // Prepare - The service
      var config = new IpFromArgumentsConfiguration(){Arguments = null,
                                                      ArgumentIndex = 0};
      var service = new IpFromArgumentsService(config, logger);

      // act
      Func<Task>act = async () => await service.GetIP();

      // assert
      var exception = await Assert.ThrowsAsync<NoArgumentsException>(act);
    }

    [Fact]
    public async Task
    Throws_exceptions_on_negative_argument_indexAsync() {
      // Prepare - The service
      var config = new IpFromArgumentsConfiguration(){Arguments = null,
                                                      ArgumentIndex = -1};
      var service = new IpFromArgumentsService(config, logger);

      // act
      Func<Task>act = async () => await service.GetIP();

      // assert
      var exception = await Assert.ThrowsAsync<NegativeIndexException>(act);
    }

    [Fact]
    public async Task
    Throws_exceptions_on_out_of_bounds_argument_indexAsync() {
      // Prepare - The service
      var config = new IpFromArgumentsConfiguration(){
          Arguments = ArgumentsList, ArgumentIndex = ArgumentsList.Count()};
      var service = new IpFromArgumentsService(config, logger);

      // act
      Func<Task>act = async () => await service.GetIP();

      // assert
      var exception = await Assert.ThrowsAsync<IndexOutOfBoundsException>(act);
      Assert.NotNull(exception);
      Assert.Equal(config.Arguments.Count, exception.ElementsCount);
      Assert.Equal(config.ArgumentIndex, exception.RequestedIndex);
    }
  }
}
