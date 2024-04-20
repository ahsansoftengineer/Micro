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
      CreateMap<PlatformPublishedDto, Platform>()
        .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
    }
  }
}