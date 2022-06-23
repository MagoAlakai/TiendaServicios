#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaLibros.Client/TiendaLibros.Client.csproj", "TiendaLibros.Client/"]
COPY ["TiendaServicios.Contracts/TiendaServicios.Contracts.csproj", "TiendaServicios.Contracts/"]
RUN dotnet restore "TiendaLibros.Client/TiendaLibros.Client.csproj"
COPY . .
WORKDIR "/src/TiendaLibros.Client"
RUN dotnet build "TiendaLibros.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaLibros.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaLibros.Client.dll"]