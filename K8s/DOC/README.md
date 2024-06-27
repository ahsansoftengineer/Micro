### [MEDIUM GUIDE](https://medium.com/c-sharp-progarmming/working-with-grpc-in-dotnet-86c80c1e7b3)

### KUBERNETES COMMANDS
- Kubernetes always start deployments from Docker Hub

#### KUBERNETES SETTINGS
1. Enable the Kubernetes from Docker Destop Settings
```bash
# Install the Below Package in Ubuntu
sudo snap install kubectl --classic
kubectl version

# Other Commands
kubectl get pods
kubectl get deployments
kubectl get services
kubectl get namespace
kubectl get storageclass
kubectl get pvc

# HARD RESTART DEPLOYMENTS
kubectl rollout restart deployments # Restart all deployments
kubectl rollout restart deployments name-depl # Restart 1 Deployments
kubectl rollout restart deployments name-depl1 name-depl2
```
#### POINTS
- Cluster IPs using for Interservice comm
- Every Pod has 1 Container
- Every Container has 1 Application
- Every Pod has Cluster Internal IP
- Some Pod has Cluster External IP
- DB Pod has Persisten Volume Claim


#### PLATFORM SERVICE
- API Service
```bash
docker build -t ahsansoftengineer/platformservice ../PlatformService
docker push ahsansoftengineer/platformservice
kubectl delete deployments depl-platforms
kubectl apply -f depl-platforms.yaml
kubectl rollout restart deployments depl-platforms
# http://ahsan.host.c
```

#### COMMAND SERVICE
- API Service talking via TCP
```bash
docker build -t ahsansoftengineer/commandservice ../CommandsService
docker push ahsansoftengineer/commandservice
kubectl delete deployments depl-commands
kubectl apply -f depl-commands.yaml
kubectl rollout restart deployments depl-commands
```

#### PLATFORM NODE PORT
- External Port 
```bash
kubectl apply -f srv-np-platforms.yaml
kubectl delete services srv-np-platforms
kubectl get services

# ACCESS APPLICATION
## OVER NODE PORT
# deployment.apps/depl-platforms unchanged
# service/srv-clusterip-platforms created
# http://localhost:31971/api/platform
```

#### PERSISTENT VOLUME CLAIMS
- To Keep Data Persistent even Application Stop
- When Ever you Change the Claims you have to update its name
- And Update this in the Deployment File
- depl-mssql-plat.yaml
- pvc-local.yaml
```yaml
metadata:
  name: claimss-mssql
```
```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
kubectl apply -f pvc-local.yaml
kubectl get pvc
```

#### MS SQL SERVICE 
- Database with load Balancer
```bash
kubectl apply -f depl-mssql-plat.yaml
kubectl delete deployments depl-mssql-plat
kubectl get deployments
kubectl get services
```

#### INGRESS NGINX (Pod, Container & Loadbalancer)
- External Access of Servicies
- - Platform Service
- - Sql Database
- Work as a Load Balancer & API Getway
- This is Kubernetes Pod
- It will run Several Pods in Docker Desktop
```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml
kubectl get namespace
kubectl delete namespace ingress-nginx
kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx

kubectl delete ingress srv-ingress
kubectl apply -f srv-ingress.yaml
# ACCESS APPLICATION 
## OVER INGRESS NGINX (DNS)
# Routing file srv-ingress.yaml
# Test the Application on 
# http://ahsan.host.com/swagger/index.html
# http://ahsan.host.com/api/platforms
# http://ahsan.host.com/api/c/platforms
```
#### MSG BRKR RABBIT MQ
```bash
kubectl apply -f depl-rabbitmq.yaml
kubectl delete deployments depl-rabbitmq

# http://localhost:15672/
# http://ahsan.host.com:15672/#/exchanges
# username: guest
# password: guest

## APPLICATION ENV
# "RabbitMQHost": "srv-clusterip-rabbitmq",
# "RabbitMQPort": "5672"
```


#### NOTE 
- When Changing Naming Convention do change cluster ips in the application where it used
```json
{
  "CommandService" : "http://srv-clusterip-commands:8082/api/c/platforms",
  "ConnectionStrings": {
    "PlatformConn" : "Server=srv-clusterip-mssql,1433;Initial Catalog=platformsdb;User ID=sa;Password=pa55w0rd!;;TrustServerCertificate=true"
  }
}


```
#### KUBERNETES STORAGE CLASS
- Persistent Volume Claims
- Persistent Volume
- Storage Class

#### AZURE DATA STUDIO
- Download Azure Data Studio
```bash
cd ~
sudo dpkg -i ./Downloads/azuredatastudio-linux-1.48.0.deb
azuredatastudio
# Azure Data Studio
# Connection Type         : Microsfot SQL Server
# Input Type              : Parameters
# Server                  : localhost,1433
# Authentication Type     : SQL Login
# User Name               : sa
# Password                : pa55w0rd!
# Encrypt                 : Mandatory
# Trust Server Certifcate : True
# Rest is Default
```

#### SSH KUBERNATES CONTAINER

```bash
# Kubernates Pods are not Virtual Machines
# SSH Pods
kubectl get pods
kubectl exec -it <pod-name> -- /bin/sh # Example
kubectl exec -it depl-commands-d4bff94dd-cwlrg -- /bin/sh # Tested 

# GET Pods inside NameSpace
kubectl get pods --namespace=ingress-nginx

# GET Services inside NameSpace
kubectl get services --namespace=ingress-nginx 

# GET Containers inside Pods
kubectl get pods depl-commands-d4bff94dd-cwlrg -o jsonpath='{.spec.containers[*].name}'

# SSH Pods inside NameSpace
kubectl get pods --namespace=ingress-nginx
kubectl exec -it -n <name space here> <pod-name> -- /bin/sh # Example
kubectl exec -it -n ingress-nginx ingress-nginx-controller-7dcdbcff84-6dr2r -- /bin/sh # Tested


# SSH Container Inside Pod & NameSpace
# 1 Get Pods inside NameSpace
kubectl get pods --namespace=ingress-nginx 
# 2 GET Container Inside Pod & NameSpace
kubectl get pods ingress-nginx-controller-7dcdbcff84-6dr2r --namespace=ingress-nginx -o jsonpath='{.spec.containers[*].name}'
# 3 SSH Container Inside Pod & NameSpace
kubectl exec -it -n <NAMESPACE> <POD_NAME> --container <CONTAINER_NAME> -- /bin/bash # Example
kubectl exec -it -n ingress-nginx ingress-nginx-controller-7dcdbcff84-6dr2r --container controller -- /bin/bash # Tested
```

### GRPC
- Platform depl-platforms.yaml
```yml
  - name: platformgrpc
    protocol: TCP
    port: 666
    targetPort: 666
```
- Platform appsettings.Production.json
```json
  "Kestrel": {
    "Endpoints": {
      "Grpc": {
        "Protocols": "Http2",
        "Url": "http://srv-clusterip-platforms:666"
      },
      "webApi":{
        "Protocols": "Http1",
        "Url": "http://srv-clusterip-platforms:8080"
      }
    }
  }
```
