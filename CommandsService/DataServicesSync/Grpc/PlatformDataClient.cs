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
        return _map.Map<IEnumerable<Platform>>(reply.Platform);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"--> Could not call GRPC Server {ex.StackTrace}");
        return null;
      }
    }
  }
}