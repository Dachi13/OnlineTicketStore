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
COPY ["Services/Event/Event.csproj", "Services/Event/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
RUN dotnet restore "Services/Event/Event.csproj"
COPY . .
WORKDIR "/src/Services/Event"
RUN dotnet build "Event.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Event.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Event.dll"]

## Set base image
#FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
## Configure HTTPS
#ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword
#
## Build section
#FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["Services/Catalog/Catalog.csproj", "Services/Catalog/"]
#COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
#RUN dotnet restore "Services/Catalog/Catalog.csproj"
#COPY . .
#WORKDIR "/src/Services/Catalog"
#RUN dotnet build "Catalog.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## Publish section
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "Catalog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Final image
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Catalog.dll"]