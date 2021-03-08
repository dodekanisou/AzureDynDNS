#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/AzureDynDns/AzureDynDns.csproj", "src/AzureDynDns/"]
RUN dotnet restore "src/AzureDynDns/AzureDynDns.csproj"
COPY . .
WORKDIR "/src/src/AzureDynDns"
RUN dotnet build "AzureDynDns.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzureDynDns.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["scheduler.sh"]