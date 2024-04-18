using AutoMapper;
using PlatformService.DTO;
using PlatformService.MODELS;

namespace PlatformService.Profiles
{
  public class PlatformsProfile : Profile 
  {
    public PlatformsProfile() 
    {
      CreateMap<Platform, PlatformReadDto>();
      CreateMap<PlatformCreateDto, Platform>();
      CreateMap<PlatformReadDto, PlatformPublishedDto>();
    }
  }
}