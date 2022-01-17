FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ElasticSearchLogging.Api/ElasticSearchLogging.Api.csproj", "ElasticSearchLogging.Api/"]
RUN dotnet restore "ElasticSearchLogging.Api/ElasticSearchLogging.Api.csproj"
COPY . .
WORKDIR "/src/ElasticSearchLogging.Api"
RUN dotnet build "ElasticSearchLogging.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearchLogging.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticSearchLogging.Api.dll"]
