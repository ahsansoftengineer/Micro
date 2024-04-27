### appsettings.json
```json
# Production
  "GrpcPlatform" : "http://srv-clusterip-platforms:666"
# Dev
  "GrpcPlatform": "https://localhost:5001"
```

### Packages and Config csproj
- Configuration for Client
```xml
<PackageReference Include="Grpc.AspNetCore" Version="2.62.0" />
<PackageReference Include="Grpc.Net.Client" Version="2.62.0" />
<PackageReference Include="Grpc.Tools" Version="2.62.0">
  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  <PrivateAssets>all</PrivateAssets>
</PackageReference>
```
- Configuration
```xml
<ItemGroup>
  <Protobuf Include="Protos\platforms.proto" GrpcServices="Client" />
</ItemGroup>
```

### Protos
- Create the file Save the changes and build / run & stop the project
- Dtos and Model should be availaible as per Proto
```proto
syntax = "proto3";

option csharp_namespace = "PlatformService";

service GrpcPlatform {
    rpc GetAllPlatforms (GetAllRequest) returns (PlatformResponse);
}

message GetAllRequest {}

message GrpcPlatformModel{
    int32 platformId = 1;
    string name = 2;
    string publisher = 3;
}

message PlatformResponse {
    repeated GrpcPlatformModel platform = 1;
}
```

### Mapping (DTOs)
- Dtos and Model should be availaible as per Proto
- Profile Files
```c#
CreateMap<PlatformPublishedDto, Platform>()
  .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
CreateMap<GrpcPlatformModel, Platform>()
  .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
  .ForMember(dest => dest.Commands, opt => opt.Ignore());
```
### Data Service Sync

```csharp
using AutoMapper;
using CommandsService.MODELS;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.DataServicesSync.Grpc
{
  public class PlatformDataClient : IPlatformDataClient
  {
    private readonly IConfiguration _config;
    private readonly IMapper _map;

    public PlatformDataClient(IConfiguration config, IMapper map)
    {
      _config = config;
      _map = map;

    }

    public IEnumerable<Platform> ReturnAllPlatforms()
    {
      Console.WriteLine($"--> Calling GRPC Service {_config["GrpcPlatform"]}");
      var channel = GrpcChannel.ForAddress(_config["GrpcPlatform"]);
      var client = new GrpcPlatform.GrpcPlatformClient(channel);
      var req = new GetAllRequest();

      try
      {
        var reply = client.GetAllPlatforms(req);
        // return 
      }
      catch (Exception ex)
      {
        Console.WriteLine($"--> Could not call GRPC Server {ex.Message}");
      }
      
      throw new NotImplementedException();
    }
  }
}
```

### STARTUP.cs Configure
```csharp
services.AddScoped<IPlatformDataClient, PlatformDataClient>();
```