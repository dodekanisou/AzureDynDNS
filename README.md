# AzureDynDNS

An Azure based DynDNS alternative.

![Compile AzureDynDNS](https://github.com/dodekanisou/AzureDynDNS/workflows/Compile%20AzureDynDNS/badge.svg)
[![Total alerts](https://img.shields.io/lgtm/alerts/g/dodekanisou/AzureDynDNS.svg?logo=lgtm&logoWidth=18)](https://lgtm.com/projects/g/dodekanisou/AzureDynDNS/alerts/)

This .net core 3.1 console application can run both interactively or as a
daemon.

Retrieves the IP from one of the supported IP providers and updates an A record
in the Azure DNS service registered in the configuration.

Currently supported IP providers:

- [Public IP provided by ipify](docs/ip-sources/ipify-public-ip.md)
- [Command line argument](docs/ip-sources/command-line-argument.md)

## Setup instructions

### Azure infrastructure

Deploy an Azure DNS zone using either the portal or an
[ARM template](https://github.com/Azure/azure-quickstart-templates/tree/master/101-azure-dns-new-zone).

Provision an Azure Active Directory Application and assign 'DNS Zone
Contributor' permissions by following
[this guide](docs/create-aad-application.md).

### Application installation

> Although this guide is for linux, the utility works on windows as well.

Unzip the release into `/var/lib/azureddns`.

```bash
sudo unzip publish.zip -d /var/lib/azureddns
```

Ensure that the AzureDynDns executable has the required execute permission:

```bash
sudo chmod +x /var/lib/azureddns/AzureDynDns
```

Add an `appsettings.json` file next to the executable:

```bash
sudo nano /var/lib/azureddns/appsettings.json
```

Your `appsettings.json` file should look like the following (GUIDs are randomly
generated in the following example using
[this tool](https://www.guidgenerator.com/online-guid-generator.aspx)):

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

## Scenarios

The following scenarios have been tested:

- [Schedule to run on RPI 2b with ubuntu](docs/scenarios/rpi-ubuntu-service.md)
- [After connecting to OpenVPN](docs/scenarios/openvpn-up-cmd.md)
