using CommandsService.Dtos;

namespace PlatformService.SyncDataServices.Http 
{
  public interface ICommandDataClient 
  {
    Task SendPlatformToCommand(PlatformReadDto platform);
  }
}