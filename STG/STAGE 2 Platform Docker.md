### Dockerizing Platform Service
0. Mapping Application to work on 80 Port for Docker 
- Host networking allows containers that are started with --net=host to use localhost to connect to TCP and UDP services on the host. It will automatically allow software on the host to use localhost to connect to TCP and UDP services in the container.


```c#
webBuilder.UseUrls("http://0.0.0.0:8080");
```
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

EXPOSE 8080:8080

ENTRYPOINT [ "dotnet", "PlatformService.dll"]
```
2. Utilizing Image
```bash
docker build -t ahsansoftengineer/platformservice2 .
docker build -t ahsansoftengineer/platformservice2 ../PlatformService
# First External Port : Internal Port
# This -p refer to publish on TCP Port
# EXPOSE is also Refer to TCP Port
docker run -p 8080:8080 -d ahsansoftengineer/platformservice2 --network bridge
docker stop 1234
docker start 1234
docker rm 1234 --force
docker push ahsansoftengineer/platformservice2
docker exec -it 1234 bash
docker exec -it great_shaw bash
```
- http://localhost:8080/api/platform