using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.DATA;
using PlatformService.Dtos;
using PlatformService.MODELS;

namespace PlatformService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PlatformController : ControllerBase
  {
    private readonly IPlatformRepo _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PlatformController> _logger;

    public PlatformController(
      IPlatformRepo repo, 
      IMapper mapper, 
      ILogger<PlatformController> logger)
    {
      _repo = repo;
      _mapper = mapper;
      _logger = logger;
    }
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      var list = _repo.GetAllPlatforms();
      Console.WriteLine(list);
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(list));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
      var item = _repo.GetPlatformById(id);
      Console.WriteLine(item);
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
    public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto dto)
    {
      var model = _mapper.Map<Platform>(dto);
      _repo.CreatePlatform(model);
      _repo.SaveChanges();
      var read = _mapper.Map<PlatformReadDto>(model);
      // return Ok(read);
      return CreatedAtRoute(
        nameof(GetPlatformById), 
        new { Id = read.Id},
        read
      );
    }

  }
}