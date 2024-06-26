using System.Collections;
using CommandsService.MODELS;

namespace CommandsService.DATA 
{
  public interface ICommandRepo 
  {
    bool SaveChanges();

    // Platforms
    IEnumerable<Platform> GetAllPlatforms();
    void CreatePlatform(Platform platform);
    bool PlatformExists(int platformId);
    bool ExternalPlatformExist(int externalId);

    // Commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command? GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);  
  }
}