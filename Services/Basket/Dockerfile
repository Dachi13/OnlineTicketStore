FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Configure HTTPS
ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket/Basket.csproj", "Services/Basket/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
RUN dotnet restore "Services/Basket/Basket.csproj"
COPY . .
WORKDIR "/src/Services/Basket"
RUN dotnet build "Basket.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Basket.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.dll"]