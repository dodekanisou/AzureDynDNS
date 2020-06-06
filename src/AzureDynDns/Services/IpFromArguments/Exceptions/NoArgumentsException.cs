using System;

namespace AzureDynDns.Services.IpFromArguments.Exceptions {
public class NoArgumentsException : Exception {
    public NoArgumentsException() : base("Arguments: No arguments found.") {}
}
}
