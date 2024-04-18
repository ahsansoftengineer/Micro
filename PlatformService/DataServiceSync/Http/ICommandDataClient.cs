
using PlatformService.DTO;

namespace PlatformService.DataServiceSyncs.Http 
{
  public interface ICommandDataClient 
  {
    Task SendPlatformToCommand(PlatformReadDto platform);
  }
}