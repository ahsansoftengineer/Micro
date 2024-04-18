### Local Dev
```bash
dotnet run
# http://localhost:5001/api/platform
# http://localhost:5001/swagger/index.html
```
### Docker Env
```bash

### DOTNET CORE DOCKER
```bash
docker build -t ahsansoftengineer/platformservice .
docker run -p 8081:8080 -d ahsansoftengineer/platformservice
# http://localhost:8081/api/platform
# http://localhost:8081/swagger/index.html
docker ps # -a 
docker container stop container_id
docker container start container_id
docker push ahsansoftnegineer/platformservice
```
### K8s
- In Case of K8s Both the Files needs to be run at the same time
```bash
# DEPL-PLATFORM.YAML
kubectl apply -f depl-platforms.yaml
kubectl get pods
kubectl get deployments
kubectl delete deployments plastforms-depl


# NODE PORT (SRV-NP-PLATFORMS.YAML)
kubectl apply -f srv-np-platforms.yaml
kubectl get services

# http://localhost:31971/api/platform
# http://localhost:31971/swagger/index.html
```