# build the image only: docker build -t name:TiendaServicios.Api.Identity -f TiendaServicios.Api.Identity/Dockerfile .
# docker image ls
# docker image tag <image-id/> api/identity:latest
# docker tag api/identity:latest magoalakai/tienda-servicios-api-identity:latest
# docker push magoalakai/tienda-servicios-api-identity:latest

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 53449

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Identity/TiendaServicios.Api.Identity.csproj", "TiendaServicios.Api.Identity/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaServicios.Api.Identity/TiendaServicios.Api.Identity.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Identity"
RUN dotnet build "TiendaServicios.Api.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Identity.dll"]