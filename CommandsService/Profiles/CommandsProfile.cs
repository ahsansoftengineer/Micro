using AutoMapper;
using CommandsService.DTO;
using CommandsService.MODELS;

namespace CommandsService.Profiles
{
  public class CommandsProfile : Profile
  {
    public CommandsProfile() 
    {
      // Source -> Target
      CreateMap<Platform, PlatformReadDto>();
      CreateMap<CommandCreateDto, Command>();
      CreateMap<Command, CommandReadDto>();
      // 6:45:32 Video Secs
    }
  }
}