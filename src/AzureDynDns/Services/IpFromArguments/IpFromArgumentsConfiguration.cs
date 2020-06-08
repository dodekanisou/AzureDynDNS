using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDynDns.Services.IpFromArguments
{
public class IpFromArgumentsConfiguration
{
    /// <summary>
    /// The index of the argument to use as the IP.
    /// </summary>
    public int ArgumentIndex
    {
        get;
        set;
    }

    /// <summary>
    /// The arguments passed to the application.
    /// </summary>
    public IList<string> Arguments
    {
        get;
        set;
    }
}
}
