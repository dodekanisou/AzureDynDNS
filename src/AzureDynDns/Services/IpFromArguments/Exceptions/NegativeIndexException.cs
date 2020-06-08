using System;

namespace AzureDynDns.Services.IpFromArguments.Exceptions
{
public class NegativeIndexException : Exception
{
    public NegativeIndexException()
        : base("ArgumentIndex: Invalid index. Less than zero.")
    {
    }
}
}
