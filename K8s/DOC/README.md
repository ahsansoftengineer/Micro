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
kubectl rollout restart deployments name-depl
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
kubectl apply -f platforms-depl.yaml
kubectl delete deployments platforms-depl
kubectl rollout restart deployments platform-depl
```

#### COMMAND SERVICE
- API Service talking via TCP
```bash
kubectl apply -f commands-depl.yaml
kubectl delete deployments commands-depl
```

#### PLATFORM NODE PORT
- External Port 
```bash
kubectl apply -f platforms-np-srv.yaml
kubectl get services

# ACCESS APPLICATION
## OVER NODE PORT
# deployment.apps/platforms-depl unchanged
# service/platforms-clusterip-srv created
# http://localhost:31971/api/platform
```

#### PERSISTENT VOLUME CLAIMS
- To Keep Data Persistent even Application Stop
- When Ever you Change the Claims you have to update its name
- And Update this in the Deployment File
- mssql-plat-depl.yaml
- local-pvc.yaml
```yaml
metadata:
  name: mssql-claimss
```
```bash
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!",
kubectl apply -f local-pvc.yaml
kubectl get pvc
```

#### MS SQL SERVICE 
- Database with load Balancer
```bash
kubectl apply -f mssql-plat-depl.yaml
kubectl get deployments
kubectl get service
```

#### INGRESS NGINX (Pod, Container & Loadbalancer)
- External Access of Servicies
- - Platform Service
- - Sql Database
- Work as a Load Balancer & API Getway
- This is Kubernetes Pod
- It will run Seveleral Pods in Docker Desktop
```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.0/deploy/static/provider/cloud/deploy.yaml
kubectl get namespace
kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx

# ACCESS APPLICATION 
## OVER INGRESS NGINX (DNS)
# Routing file ingress-srv.yaml
# Test the Application on 
# http://ahsan.host.com/swagger/index.html
# http://ahsan.host.com/api/platforms
# http://ahsan.host.com/api/c/platforms
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
