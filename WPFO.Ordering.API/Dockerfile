#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO.Ordering.API/WPFO.Ordering.API.csproj", "WPFO.Ordering.API/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
COPY ["EventBus.Messages/EventBus.Messages.csproj", "EventBus.Messages/"]
COPY ["WPFO.Ordering.Application/WPFO.Ordering.Application.csproj", "WPFO.Ordering.Application/"]
COPY ["WPFO.Core/WPFO.Core.csproj", "WPFO.Core/"]
COPY ["WPFO.Ordering.Domain/WPFO.Ordering.Domain.csproj", "WPFO.Ordering.Domain/"]
COPY ["WPFO.OrderIng.Infrastructure/WPFO.OrderIng.Infrastructure.csproj", "WPFO.OrderIng.Infrastructure/"]
RUN dotnet restore "WPFO.Ordering.API/WPFO.Ordering.API.csproj"
COPY . .
WORKDIR "/src/WPFO.Ordering.API"
RUN dotnet build "WPFO.Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.Ordering.API.dll"]