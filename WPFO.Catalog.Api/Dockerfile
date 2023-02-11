#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO.Catalog.Api/WPFO.Catalog.Api.csproj", "WPFO.Catalog.Api/"]
RUN dotnet restore "WPFO.Catalog.Api/WPFO.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/WPFO.Catalog.Api"
RUN dotnet build "WPFO.Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.Catalog.Api.dll"]

