#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Compentio.Ferragosto.Reports/Compentio.Ferragosto.Reports.csproj", "Compentio.Ferragosto.Reports/"]
RUN dotnet restore "Compentio.Ferragosto.Reports/Compentio.Ferragosto.Reports.csproj"
COPY . .
WORKDIR "/src/Compentio.Ferragosto.Reports"
RUN dotnet build "Compentio.Ferragosto.Reports.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compentio.Ferragosto.Reports.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compentio.Ferragosto.Reports.dll"]