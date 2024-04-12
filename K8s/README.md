### KUBERNETES COMMANDS
1. Enable the Kubernetes from Docker Destop Settings
```bash
# Install the Below Package in Ubuntu
sudo snap install kubectl --classic
kubectl version

kubectl apply -f platforms-depl.yaml
kubectl get pods
kubectl get deployments
kubectl delete deployments platforms-depl

```


### NODE PORT
```bash
kubectl apply -f platforms-np-srv.yaml
kubectl get services
```