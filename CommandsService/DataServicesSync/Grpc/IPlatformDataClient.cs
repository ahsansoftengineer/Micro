using CommandsService.MODELS;

namespace CommandsService.DataServicesSync.Grpc
{
  public interface IPlatformDataClient
  {
    IEnumerable<Platform> ReturnAllPlatforms();
  }
}