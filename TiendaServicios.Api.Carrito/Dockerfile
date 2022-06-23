# build the image only: docker build -t name:TiendaServicios.Api.Carrito -f TiendaServicios.Api.Carrito/Dockerfile .
# docker image ls
# docker image tag <image-id/> api/carrito:latest
# docker tag api/carrito:latest magoalakai/tienda-servicios-api-carrito:latest
# docker push magoalakai/tienda-servicios-api-carrito:latest

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 53447

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Carrito/TiendaServicios.Api.Carrito.csproj", "TiendaServicios.Api.Carrito/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaServicios.Api.Carrito/TiendaServicios.Api.Carrito.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Carrito"
RUN dotnet build "TiendaServicios.Api.Carrito.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Carrito.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Carrito.dll"]