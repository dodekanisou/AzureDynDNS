#!/bin/sh
while :; do 
   dotnet AzureDynDns.dll
   sleep 1h & wait $!;
done
