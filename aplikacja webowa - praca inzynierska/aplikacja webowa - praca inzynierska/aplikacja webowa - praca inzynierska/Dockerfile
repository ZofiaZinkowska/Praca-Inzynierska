FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /src
COPY ["aplikacja webowa - praca inzynierska.csproj", "."]
RUN dotnet restore "aplikacja webowa - praca inzynierska.csproj"

COPY [".", "."]
RUN dotnet build "aplikacja webowa - praca inzynierska.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "aplikacja webowa - praca inzynierska.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base as final 
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","aplikacja webowa - praca inzynierska.dll"]