#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO/WPFO.UI.csproj", "WPFO/"]
COPY ["WPFO.OrderIng.Infrastructure/WPFO.OrderIng.Infrastructure.csproj", "WPFO.OrderIng.Infrastructure/"]
COPY ["WPFO.Core/WPFO.Core.csproj", "WPFO.Core/"]
COPY ["WPFO.Ordering.Domain/WPFO.Ordering.Domain.csproj", "WPFO.Ordering.Domain/"]
COPY ["WPFO.Ordering.Application/WPFO.Ordering.Application.csproj", "WPFO.Ordering.Application/"]
RUN dotnet restore "WPFO/WPFO.UI.csproj"
COPY . .
WORKDIR "/src/WPFO"
RUN dotnet build "WPFO.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.UI.dll"]