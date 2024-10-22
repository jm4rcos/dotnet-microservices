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
    [HttpPost]
    public ActionResult TestInboundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command Service");
      return Ok("Inbound test of from Platforms Controller");
    }
  }
}