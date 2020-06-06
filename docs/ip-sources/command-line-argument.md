# IP source: Command line arguments

You can set the dns record to an IP address passed in the arguments.

Imagine that you have an application that is invoking the AzureDynDns executable with the following arguments:

```bash
/var/lib/azureddns/AzureDynDns tun0 1500 1551 10.0.0.2 255.255.255.0 init
```

You want to update the Azure DNS record with the following IP `10.0.0.2` which is the 4th argument<sup>1</sup>.

To do that, you need to specify the following parameters in the `appsettings.json` file:

```json
{
  "Settings": {
    "IPSource": "arguments",
    "ArgumentIndex": 4
  }
}
```

The scenario that drove the development of this IP source provider is to be able to [get the private IP after connecting to OpenVPN](../scenarios/openvpn-up-cmd.md).

<sup>1</sup> In reality this is the 5th argument the application sees but since we are using argument index in the configuration, we will configure the application with `ArgumentIndex` 4.
