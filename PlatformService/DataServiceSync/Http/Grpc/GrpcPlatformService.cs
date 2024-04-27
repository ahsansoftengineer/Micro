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