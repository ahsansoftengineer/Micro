
### GRPC with PLATFORMS
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


