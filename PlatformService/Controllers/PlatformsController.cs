using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DATA;
using PlatformService.DTO;
using PlatformService.MODELS;
using PlatformService.DataServiceSyncs.Http;
using PlatformService.DataServiceAsync;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformController : ControllerBase
  {
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PlatformController> _logger;
    private readonly ICommandDataClient _cmdDataClient;
    private readonly IMessageBusClient _messageBusClient;

    public PlatformController(
      IPlatformRepo repo,
      IMapper mapper,
      ICommandDataClient cmdDataClient,
      ILogger<PlatformController> logger,
      IMessageBusClient messageBusClient)
    {
      _repo = repo;
      _mapper = mapper;
      _logger = logger;
      _cmdDataClient = cmdDataClient;
      _messageBusClient = messageBusClient;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      var list = _repo.GetAllPlatforms();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(list));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
      var item = _repo.GetPlatformById(id);
      if (item != null)
      {
        return Ok(_mapper.Map<PlatformReadDto>(item));
      }
      else
      {
        return NotFound();
      }
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto dto)
    {
      var model = _mapper.Map<Platform>(dto);
      _repo.CreatePlatform(model);
      _repo.SaveChanges();
      var read = _mapper.Map<PlatformReadDto>(model);
      // return Ok(read);

      // Send Sync Message
      try
      {
        await _cmdDataClient.SendPlatformToCommand(read);
      }
      catch (Exception ex)
      {
        Console.WriteLine("--> Could not send synchronously " + ex.Message);
      }

      // Send Async Message
      try
      {
        var pubDto = _mapper.Map<PlatformPublishedDto>(read);
        pubDto.Event = "Platform_Published";
        _messageBusClient.PublishNewPlatform(pubDto);
      }
      catch (Exception ex)
      {
        Console.WriteLine("--> Could not send Async Msg: " + ex.Message);
      }

      return CreatedAtRoute(
        nameof(GetPlatformById),
        new { Id = read.Id },
        read
      );
    }

  }
}