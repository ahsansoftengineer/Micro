# EXPORT BaseImage AS Build-ENV
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore


COPY . ./
RUN dotnet publish -c Release -o out

# BUILDING FUL RUN TIME VERSION
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build-env /app/out .

ENTRYPOINT [ "dotnet", "PlatformService.dll"]




