FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["dotnet-crud-api.csproj", "./"]
RUN dotnet restore "dotnet-crud-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "dotnet-crud-api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "dotnet-crud-api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# copy certificate to docker, to allow https locally (certificate password -> dotnet8)
USER root
RUN mkdir -p /app/certificates
COPY https/aspnetapp.pfx /app/https/

ENTRYPOINT ["dotnet", "dotnet-crud-api.dll"]

# BUILD: docker build -t dotnet-api .
