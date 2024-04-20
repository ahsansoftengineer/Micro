using System.Text.Json;
using AutoMapper;
using CommandsService.DATA;
using CommandsService.DTO;
using CommandsService.MODELS;

namespace CommandsService.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(
    IServiceScopeFactory scopeFactory,
    IMapper mapper
  )
    {
      _scopeFactory = scopeFactory;
      _mapper = mapper;
    }
    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch (eventType)
      {
        case EventType.PlatformPublished:
          // Todo
          // addPlatform(message);
          break;
        default:
          break;
      }
    }
    private EventType DetermineEvent(string notification){
      Console.WriteLine("--> Determining Event");
      var eventType = JsonSerializer.Deserialize<GenericEventDto>(notification);
      
      // Declare in Platform Service
      switch (eventType.Event)
      {
        case "Platform_Published":
          Console.WriteLine("--> Platform Publised Event Detected");
          return EventType.PlatformPublished;
        default:
          Console.WriteLine("--> Could not determine the event type");
          return EventType.Undetermined;
      }
   
    }
    private void addPlatform(string platformPublishedMessage)
    {
      using(var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
          var plat = _mapper.Map<Platform>(platformPublishedDto);
          if(!repo.ExternalPlatformExist(plat.ExternalId))
          {
            repo.CreatePlatform(plat);
            repo.SaveChanges(); 
          }
          else 
          {
            Console.WriteLine("--> Platform already exists...");
          }
        }
        catch(Exception ex)
        {
          Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");

        }
      }
    }
  }
  enum EventType
  {
    PlatformPublished,
    Undetermined
  }
}