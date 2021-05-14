#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SensorService/SensorService.csproj", "SensorService/"]
RUN dotnet restore "SensorService/SensorService.csproj"
COPY . .
WORKDIR "/src/SensorService"
RUN dotnet build "SensorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ADD SensorService/DataSource/SolarPredictionTestShort.csv /app/DataSource/
ENTRYPOINT ["dotnet", "SensorService.dll"]