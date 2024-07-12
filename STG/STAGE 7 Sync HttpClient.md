### SYNC MESSAGING HTTP CLIENT
1.  From a messaging perspective async method is synchronous
2. The Client still has to wait for a response
3. Async in this context (the C# language) means that the action will not wait for long running operation
4. It will hand back it's thread to the thread pool, where it can be reused.
5. When the operation finishes it will re-acquire a thread and complete, (and respond back to the requestor)
6. **So Async here is about thread exhaustion - the requestor still has to wait (the call is synchronous)





#### Creating CommandService Project
```bash
dotnet new project CommandService
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

#### ADDING PLATFORM CONTROLLER

```c#
namespace CommandsService.Controllers
{

  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepo repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;

    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatform() 
    {
      Console.WriteLine("--> Getting Platforms from Commands");
      // return Ok("Hello");
      var platformItems = _repo.GetAllPlatforms();
      return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }


    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command Service");

      return Ok("Inbound test of from Platforms Controller");
    }
  }
}
```