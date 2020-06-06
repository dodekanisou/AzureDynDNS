# Running on a ubuntu based Raspberry PI as a service

Update the `appsettings.json` with the following settings that you gathered from the installation instructions (note the use of `ipify` as the IP source):

```json
{
  "Settings": {
    "ClientId": "64ace103-cdc0-404f-8f0b-a0abcc32d3e7",
    "ClientSecret": "thisisapassword",
    "TenantId": "d6a3e0ee-99a9-4fd2-bc92-7b92d915e835",
    "SubscriptionId": "78f42174-ec57-4a87-a6b5-d803f9fb3790",
    "ResourceGroupName": "dyndns-rg",
    "DnsZoneName": "zone.contoso.com",
    "ARecordName": "dynamic",
    "ARecordTTL": 60,
    "IPSource": "ipify",
    "IpifyServiceAddress": ""
  }
}
```

Create a new service definition file:

```bash
sudo nano /etc/systemd/system/azureddns.service
```

with the following content:

```ini
[Unit]
Description=Azure DynDNS service to update the A Record with the current IP

[Service]
Type=simple
ExecStart=sudo /var/lib/azureddns/AzureDynDns
WorkingDirectory=/var/lib/azureddns/
Environment=DOTNET_USER_SECRETS_FALLBACK_DIR=/var/lib/azureddns/

[Install]
WantedBy=multi-user.target
```

Create a new timer definition file:

```bash
sudo nano /etc/systemd/system/azureddns.timer
```

with the following content:

```ini
[Unit]
Description=Run Azure DynDNS every half an hour

[Timer]
OnCalendar=*-*-* *:00,30:00
Unit=azureddns.service
Persistent=true

[Install]
WantedBy=timers.target
```

and then install the service, execute and observe status:

```bash
sudo systemctl enable azureddns.timer
sudo systemctl start azureddns.timer
sudo systemctl status azureddns.timer
```

> If you modify the definition files, you will need to reload the daemon by
> `sudo systemctl daemon-reload`.

If you just need to restart the service after a new deployment:

```bash
sudo systemctl start azureddns.timer
```

[More info on how timers works](https://www.certdepot.net/rhel7-use-systemd-timers/).
