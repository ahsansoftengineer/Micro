### KUBERNETES COMMANDS

#### KUBERNETES SETTINGS
1. Enable the Kubernetes from Docker Destop Settings
```bash
# Install the Below Package in Ubuntu
sudo snap install kubectl --classic
kubectl version

# Other Commands
kubectl get pods
kubectl get deployments
```

#### PLATFORM SERVICE
```bash
kubectl apply -f platforms-depl.yaml
kubectl delete deployments platforms-depl
```

#### COMMAND SERVICE
```bash
kubectl apply -f commands-depl.yaml
kubectl delete deployments commands-depl
```

#### NODE PORT
```bash
kubectl apply -f platforms-np-srv.yaml
kubectl get services
```