### RUN LOCAL
```bash
dotnet run
# http://localhost:5001/api/platform
# http://localhost:5001/swagger/index.html

# http://localhost:6001/api/c/platforms
# http://localhost:6001/swagger/index.html
```

### RUN DOCKER CLI
```bash

### DOTNET CORE DOCKER
```bash
docker build -t ahsansoftengineer/platformservice .
docker build -t ahsansoftengineer/commandservice .
docker run -p 8081:8080 -d ahsansoftengineer/platformservice
docker run -p 8082:8080 -d ahsansoftengineer/commandservice

# http://localhost:8081/api/platform
# http://localhost:8082/api/c/platforms

# http://localhost:8081/swagger/index.html
docker ps # -a 
docker container stop container_id
docker container start container_id
docker push ahsansoftnegineer/platformservice
```
### RUN K8s
- In Case of K8s Both the Files needs to be run at the same time
```bash
# BUILD
docker build -t ahsansoftengineer/platformservice .
docker build -t ahsansoftengineer/commandservice .

# PUSH
docker push ahsansoftengineer/platformservice .
docker push ahsansoftengineer/commandservice .

# DEPL-PLATFORM.YAML
kubectl apply -f depl-platforms.yaml
kubectl apply -f depl-commands.yaml
kubectl get pods
kubectl get deployments
kubectl delete deployments plastforms-depl


# NODE PORT (SRV-NP-PLATFORMS.YAML)
kubectl apply -f srv-np-platforms.yaml
kubectl get services

```

### LOCAL HOST ROUTES (Exter Comm)
#### PLATFORM SERVICE
[GET SWAGGER](http://localhost:5001/swagger/index.html)
[GET PLATFORM](http://localhost:5001/api/platform)

#### COMMAND SERVICE
[GET SWAGGER](http://localhost:6001/swagger/index.html)
[GET PLATFORMS](http://localhost:6001/api/c/platforms)

### NODE PORT ROUTES (Inter Comm)
#### PLATFORM SERVICE
[GET PLATFORM](http://localhost:31971/api/platform)
[POST PLATFORM](http://localhost:31971/api/platform)
[GET SWAGGER](http://localhost:31971/swagger/index.html)

### CLUSTER IP ROUTES (Inter Comm)
#### PLATFORM SERVICE
- WORKS FOR INTER SERVICE COMMUNICATION
[GET PLATFORM](http://srv-clusterip-platforms:8080/api/platforms) 

#### COMMAND SERVICE
[GET PLATFORMS](http://localhost:31971/api/c/platforms)
[POST PLATFORMS/10/COMMANDS](https://localhost:31971/api/c/platforms/10/commands?howTo="asdf"&commandLine="CommandLine")
[GET PLATFORMS/10/COMMANDS](https://localhost:31971/api/c/platforms/10/commands)
[GET PLATFORMS/10/COMMANDS/7](https://localhost:31971/api/c/platforms/10/commands/7)

### DNS ROUTES (Exter Comm)
#### PLATFORM SERVICE
[GET PLATFORM](http://ahsan.host.com/api/platform)
[POST PLATFORM](http://ahsan.host.com/api/platform?)
[GET SWAGGER](http://ahsan.host.com/swagger/index.html)

#### COMMAND SERVICE
[GET PLATFORMS](http://ahsan.host.com/api/c/platforms)
[POST PLATFORMS/10/COMMANDS](https://ahsan.host.com/api/c/platforms/10/commands?howTo="asdf"&commandLine="CommandLine")
[GET PLATFORMS/10/COMMANDS](https://ahsan.host.com/api/c/platforms/10/commands)
[GET PLATFORMS/10/COMMANDS/7](https://ahsan.host.com/api/c/platforms/10/commands/7)
