# build the image only: docker build -t name:TiendaServicios.Api.Gateway -f TiendaServicios.Api.Gateway/Dockerfile .
# docker image ls
# docker image tag <image-id/> api/gateway:latest
# docker tag api/identity:latest magoalakai/tienda-servicios-api-gateway:latest
# docker push magoalakai/tienda-servicios-api-gateway:latest

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 53451

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Gateway/TiendaServicios.Api.Gateway.csproj", "TiendaServicios.Api.Gateway/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaServicios.Api.Gateway/TiendaServicios.Api.Gateway.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Gateway"
RUN dotnet build "TiendaServicios.Api.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Gateway.dll"]