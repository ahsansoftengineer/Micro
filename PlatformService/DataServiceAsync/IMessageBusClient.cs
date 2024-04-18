using PlatformService.DTO;

namespace PlatformService.DataServiceAsync
{
  public interface IMessageBusClient
  {
    void PublishNewPlatform(PlatformPublishedDto dto);
  }
}