#!/bin/sh
while :; do 
   sleep 1h & wait $!;
   dotnet AzureDynDns.dll
done