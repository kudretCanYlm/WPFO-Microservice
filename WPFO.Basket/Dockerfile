#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO.Basket/WPFO.Basket.Api.csproj", "WPFO.Basket/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
COPY ["EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["WPFO.DiscountRPC/WPFO.DiscountRPC.csproj", "WPFO.DiscountRPC/"]
RUN dotnet restore "WPFO.Basket/WPFO.Basket.Api.csproj"
COPY . .
WORKDIR "/src/WPFO.Basket"
RUN dotnet build "WPFO.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.Basket.Api.dll"]