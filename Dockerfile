#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ApiCommoditiesBr/ApiCommoditiesBr.csproj", "ApiCommoditiesBr/"]
COPY ["ApiCommoditiesBr.Core/ApiCommoditiesBr.Core.csproj", "ApiCommoditiesBr.Core/"]
COPY ["ApiCommoditiesBr.Infrastructure/ApiCommoditiesBr.Infrastructure.csproj", "ApiCommoditiesBr.Infrastructure/"]
COPY ["ApiCommoditiesBr.Helper/ApiCommoditiesBr.Helper.csproj", "ApiCommoditiesBr.Helper/"]
RUN dotnet restore "ApiCommoditiesBr/ApiCommoditiesBr.csproj"
COPY . .
WORKDIR "/src/ApiCommoditiesBr"
RUN dotnet build "ApiCommoditiesBr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiCommoditiesBr.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiCommoditiesBr.dll"]