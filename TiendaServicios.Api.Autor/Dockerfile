# build the image only: docker build -t name:TiendaServicios.Api.Autor -f TiendaServicios.Api.Autor/Dockerfile .
# docker image ls
# docker image tag <image-id/> api/autor:latest
# docker tag api/autor:latest magoalakai/tienda-servicios-api-author:latest
# docker push magoalakai/tienda-servicios-api-author:latest

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 53443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.Api.Autor/TiendaServicios.Api.Autor.csproj", "TiendaServicios.Api.Autor/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaServicios.Api.Autor/TiendaServicios.Api.Autor.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.Api.Autor"
RUN dotnet build "TiendaServicios.Api.Autor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.Api.Autor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.Api.Autor.dll"]