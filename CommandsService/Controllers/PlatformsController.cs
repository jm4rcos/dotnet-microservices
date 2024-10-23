using Microsoft.AspNetCore.Mvc;
using CommandsService.Data;
using CommandsService.Dtos;
using AutoMapper;

namespace CommandsService.Controllers
{
  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController(ICommandRepository repository, IMapper mapper) : ControllerBase
  {    
    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
      Console.WriteLine("--> Getting Platforms from Command Service");

      var platformItems = repository.GetAllPlatforms(); 
      return Ok(mapper.Map<IEnumerable<PlatformReadDto>> (platformItems));
    }

    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command Service");
      return Ok("Inbound test of from Platforms Controller");
    }
  }
}