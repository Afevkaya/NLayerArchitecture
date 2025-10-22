FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NLayerArchitecture.API/NLayerArchitecture.API.csproj", "NLayerArchitecture.API/"]
RUN dotnet restore "NLayerArchitecture.API/NLayerArchitecture.API.csproj"
COPY . .
WORKDIR "/src/NLayerArchitecture.API"
RUN dotnet build "./NLayerArchitecture.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NLayerArchitecture.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NLayerArchitecture.API.dll"]
