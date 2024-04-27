
### 1. GRPC with PLATFORMS
- Platform depl-platforms.yaml
```yml
  - name: platformgrpc
    protocol: TCP
    port: 666
    targetPort: 666
```
### 2. Platform appsettings.Production.json
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
### 3. Packages and Config
```xml
<PackageReference Include="Google.Protobuf" Version="3.26.1" />
<PackageReference Include="Grpc.AspNetCore" Version="2.62.0" />
<PackageReference Include="Grpc.Net.Client" Version="2.62.0" />
<PackageReference Include="Grpc.Tools" Version="2.62.0">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```
- Create Seperate Item Group for Generting Files
```xml
<ItemGroup>
    <Protobuf Include="Protos\platforms.proto" GrpcServices="Server" />
</ItemGroup>
```

### 3. Protos
- Create the Protos file and build and run the project to generate the files for Server
```proto
syntax = "proto3";

option csharp_namespace = "PlatformService";

service GrpcPlatform {
  rpc GetAllPlatforms (GetAllRequest) returns (PlatformResponse);
}

message GetAllRequest {}

message GrpcPlatformModel {
  int32 platformId = 1;
  string name = 2;
  string publisher = 3;
}

message PlatformResponse {
  repeated GrpcPlatformModel platform = 1;
}
```

### 3. GRPC SERVER

````csharp
using AutoMapper;
using Grpc.Core;
using PlatformService.DATA;

namespace PlatformService.DataServiceAsync.Grpc
{
  // GRPC SERVER
  public class GrpcPlatformService: GrpcPlatform.GrpcPlatformBase
  {
    private readonly IPlatformRepo _repo;
    private readonly IMapper _map;

    public GrpcPlatformService(IPlatformRepo repo, IMapper map)
    {
      _repo = repo;
      _map = map;
    }

    public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest req, ServerCallContext context)
    {
      var res = new PlatformResponse();
      var platforms = _repo.GetAllPlatforms();
      foreach (var plat in platforms)
      {
        res.Platform.Add(_map.Map<GrpcPlatformModel>(plat));
      }
      return Task.FromResult(res);
    }
  }
}

```

### Configure in STARTUP.cs file
```csharp
 app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
  // Configure Grpc Routes
  endpoints.MapGrpcService<GrpcPlatformService>();

  endpoints.MapGet("/proto/platforms.proto", async context =>
  {
    await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
  });
});

```


