# build the image only: docker build -t name:TiendaServicios.Api.Libro -f TiendaServicios.Api.Libro/Dockerfile .
# docker image ls
# docker image tag <image-id/> api/libro:latest
# docker tag api/libro:latest magoalakai/tienda-servicios-api-libro:latest
# docker push magoalakai/tienda-servicios-api-libro:latest

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 53445

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Libro/TiendaServicios.Api.Libro.csproj", "TiendaServicios.Api.Libro/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaServicios.Api.Libro/TiendaServicios.Api.Libro.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Libro"
RUN dotnet build "TiendaServicios.Api.Libro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Libro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Libro.dll"]