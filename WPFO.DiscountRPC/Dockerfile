#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WPFO.DiscountRPC/WPFO.DiscountRPC.csproj", "WPFO.DiscountRPC/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
RUN dotnet restore "WPFO.DiscountRPC/WPFO.DiscountRPC.csproj"
COPY . .
WORKDIR "/src/WPFO.DiscountRPC"
RUN dotnet build "WPFO.DiscountRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WPFO.DiscountRPC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WPFO.DiscountRPC.dll"]