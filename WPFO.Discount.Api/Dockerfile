#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO.Discount.Api/WPFO.Discount.Api.csproj", "WPFO.Discount.Api/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
RUN dotnet restore "WPFO.Discount.Api/WPFO.Discount.Api.csproj"
COPY . .
WORKDIR "/src/WPFO.Discount.Api"
RUN dotnet build "WPFO.Discount.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.Discount.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.Discount.Api.dll"]