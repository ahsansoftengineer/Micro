### K8s PlatForm Service
- Single Node, Pods, Container
- When ever container destroyed it comes back by K8s

#### App Status
- The Application Status not Change Since (5_README_DOCS) 
- Dockerfile Not Changed Since (5_1)
- New depl-platforms.yaml file Introduced

#### Dockerfile
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

#### K8s Platform Deployments Yaml File
- This file will Grab the Image from Docker Hub and Run it
```yml
# DEPLOYMENTS
apiVersion: apps/v1
kind: Deployment
metadata:
  name: depl-platforms2
spec:
  replicas: 1
  selector:
    matchLabels:
      app: platformservice2
  template:
    metadata:
      labels:
        app: platformservice2
    spec:
      containers:
      - name: platformservice2
        image: ahsansoftengineer/platformservice2:latest
        resources:
          limits:
            memory: 512Mi
            cpu: "1"
          requests:
            memory: 256Mi
            cpu: "0.2"
```
#### Running Deployments
- 
```bash
kubectl apply -f depl-platforms2.yaml
kubectl get deployments
kubectl get pods
kubectl delete deployments depl-platforms2
# kubectl delete pods depl-platforms2
```
#### Service Node Port Platform
```yaml
# NODE PORT
apiVersion: v1
kind: Service
metadata:
  name: srv-np-platforms
spec:
  type: NodePort
  selector:
    app: platformservice2
  ports:
  - name: platformservice2
    protocol: TCP
    port: 8080
    targetPort: 8080
```
#### Running Node Port Yaml
```bash
kubectl apply -f srv-np-platforms2.yaml
kubectl get services
# Get the Running Service Node Port Mapped with Deployment
curl http://localhost:30485/api/platform
```