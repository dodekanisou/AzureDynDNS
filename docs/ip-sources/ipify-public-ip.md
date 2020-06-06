# IP source: Ipify service

Utilizing the [public IP Address API](https://www.ipify.org/) you can set the dns record to the current public IP address.

In order to use this source, you need to specify the following parameters in the `appsettings.json` file:

```json
{
  "Settings": {
    "IPSource": "ipify",
    "IpifyServiceAddress": ""
  }
}
```

If you don't specify a URL in `IpifyServiceAddress`, the https://api.ipify.org url will be used.
