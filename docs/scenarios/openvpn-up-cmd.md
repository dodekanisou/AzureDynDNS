# Use the IP assigned when connecting to OpenVPN

Update the `appsettings.json` with the following settings that you gathered from the installation instructions (note the use of `arguments` as the IP source):

```json
{
   "Settings": {
           "IPSource": "arguments",
           "ArgumentIndex": 4
   }
}
```

Add the following in **OpenVPN client's configuration**:

``` config
# Allow scripts
script-security 2
# Trigger AzureDynDns during the up event
up /var/lib/azureddns/AzureDynDns
```

Connect you the vpn and look at the logs for a record like the following

```bash
Sat Jun 20 20:20:20 2020 us=12312 /var/lib/azureddns/AzureDynDns tun0 1500 1551 10.0.0.2 255.255.255.0 init
```

The Azure DNS A record should be updated with the 4th argument, e.g. `10.0.0.2`.
