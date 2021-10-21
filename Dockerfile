#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
Expose 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["LogProxyAPI.csproj", ""]
COPY ["../LogProxyAPI.Models/LogProxyAPI.Models.csproj", "../LogProxyAPI.Models/"]
COPY ["../ThirdPartyAPI.ACL/ThirdPartyAPI.ACL.csproj", "../ThirdPartyAPI.ACL/"]
RUN dotnet restore "./LogProxyAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LogProxyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogProxyAPI.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogProxyAPI.dll"]