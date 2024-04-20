using CommandsService.MODELS;

namespace CommandsService.DATA
{
  public class CommandRepo : ICommandRepo
  {
    private readonly AppDbContext _cntxt;

    public CommandRepo(AppDbContext cntxt)
    {
      _cntxt = cntxt;
    }
    public bool SaveChanges()
    {
      return _cntxt.SaveChanges() >= 0;
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _cntxt.Platforms.ToList();
    }

    public void CreatePlatform(Platform platform)
    {
      if (platform == null)
      {
        throw new ArgumentNullException(nameof(platform));
      }
      _cntxt.Platforms.Add(platform);
    }

    public bool PlatformExists(int platformId)
    {
      return _cntxt.Platforms.Any(p => p.Id == platformId);
    }
    public bool ExternalPlatformExist(int externalId)
    {
      return _cntxt.Platforms.Any(p => p.ExternalId == externalId);
    }
    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
      return _cntxt.Commands
        .Where(c => c.PlatformId == platformId)
        .OrderBy(c => c.Platform.Name);
    }

    public Command GetCommand(int platformId, int commandId)
    {
      return  _cntxt.Commands
      .Where(c => c.PlatformId == platformId)
      .FirstOrDefault();
    }

    public void CreateCommand(int platformId, Command command)
    {
      if (command == null)
      {
        throw new ArgumentNullException(nameof(command));
      }
      command.PlatformId = platformId;
      _cntxt.Commands.Add(command);
    }


  }
}