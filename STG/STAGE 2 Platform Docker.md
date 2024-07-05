### Dockerizing Platform Service

1. Docker file 
```Dockerfile
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
```
2. Making Image
```bash
docker build -t ahsansoftengineer/platformservice2 .
docker build -t ahsansoftengineer/platformservice2 ../PlatformService
# First External Port : Internal Port
# This -p refer to publish on TCP Port
# EXPOSE is also Refer to TCP Port
docker run -p 8080:80 -d ahsansoftengineer/platformservice2
docker stop 1234
docker start 1234
docker push ahsansoftengineer/platformservice2

```