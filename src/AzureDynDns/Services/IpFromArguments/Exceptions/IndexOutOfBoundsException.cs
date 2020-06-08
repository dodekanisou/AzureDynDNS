using System;

namespace AzureDynDns.Services.IpFromArguments.Exceptions
{
public class IndexOutOfBoundsException : ArgumentOutOfRangeException
{
    public IndexOutOfBoundsException(IpFromArgumentsConfiguration config)
        : base($"ArgumentIndex: Didn't pass enough arguments. " +
               $"Asked for index {config.ArgumentIndex} and got " +
               $"{config.Arguments.Count} arguments." +
               $"TIP: Index starts from zero not one.")
    {
        this.RequestedIndex = config.ArgumentIndex;
        this.ElementsCount = config.Arguments.Count;
    }

    public int RequestedIndex
    {
        get;
        set;
    }

    public int ElementsCount
    {
        get;
        set;
    }
}
}
