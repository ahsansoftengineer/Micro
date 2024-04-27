
using PlatformService.DTO;

namespace PlatformService.DataServiceSync.Http 
{
  public interface ICommandDataClient 
  {
    Task SendPlatformToCommand(PlatformReadDto platform);
  }
}