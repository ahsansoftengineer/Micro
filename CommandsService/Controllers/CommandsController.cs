using AutoMapper;
using CommandsService.DATA;
using CommandsService.DTO;
using CommandsService.MODELS;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
  [Route("api/c/platforms/{platformId}/[controller]")]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
      Console.WriteLine($"-->  Hit GetCommandsForPlatform: {platformId}");
      if (!_repo.PlatformExists(platformId))
      {
        return NotFound();
      }
      var commands = _repo.GetCommandsForPlatform(platformId);
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));

    }
    
    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
      Console.WriteLine($"Hit GetCommandForPlatform {platformId} / {commandId}");
      if (!_repo.PlatformExists(platformId))
      {
        return NotFound();
      }
      var command = _repo.GetCommand(platformId, commandId);
      if(command == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<CommandReadDto>(command));
    }
  
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto dto)
    {
      Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");
      if(!_repo.PlatformExists(platformId))
      {
        return NotFound();
      }

      var command = _mapper.Map<Command>(dto);

      _repo.CreateCommand(platformId, command); 
      _repo.SaveChanges();

      var read = _mapper.Map<CommandReadDto>(command);

      return CreatedAtRoute(nameof(GetCommandForPlatform),
      new { platformId = platformId, commandId = read.Id}, read);
    }
  }


}