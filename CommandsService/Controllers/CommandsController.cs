using Microsoft.AspNetCore.Mvc;
using CommandsService.Data;
using CommandsService.Models;
using AutoMapper;
using CommandsService.Dtos;

namespace CommandsService.Controllers
{
  [Route("api/c/platforms/{platformId}/[controller]")]
  [ApiController]
  public class CommandsController(ICommandRepository repository, IMapper mapper) : ControllerBase
  {
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
      Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");
      if (!repository.PlatformExists(platformId))
      {
        return NotFound();
      }

      var commands = repository.GetCommandsForPlatform(platformId);
      return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }

    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
      Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");
      if (!repository.PlatformExists(platformId))
      {
        return NotFound();
      }

      var command = repository.GetCommand(platformId, commandId);

      if (command == null)
      {
        return NotFound();
      }

      return Ok(mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
    {
      Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");
      if (!repository.PlatformExists(platformId))
      {
        return NotFound();
      }

      var command = mapper.Map<Command>(commandDto);

      repository.CreateCommand(platformId, command);
      repository.SaveChanges();

      var commandReadDto = mapper.Map<CommandReadDto>(command);

      return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId, commandReadDto.Id }, commandReadDto);
    }
  }
}