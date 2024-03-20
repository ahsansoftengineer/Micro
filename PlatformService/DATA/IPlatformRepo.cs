using PlatformService.MODELS;

namespace PlatformService.DATA 
{
  public interface IPlatformRepo 
  {
    bool SaveChanges();
    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int Id);
    void CreatePlatform(Platform plat);
  }
}