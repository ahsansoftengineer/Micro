using AutoMapper;
using PlatformService.Dtos;
using PlatformService.MODELS;

namespace PlatformService.Profiles
{
  public class PlatformsProfile : Profile 
  {
    public PlatformsProfile() 
    {
      CreateMap<Platform, PlatformReadDto>();
      CreateMap<PlatformCreateDto, Platform>();
    }
  }
}