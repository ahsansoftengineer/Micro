using PlatformService.MODELS;

namespace PlatformService.DATA
{
  public class PlatformRepo : IPlatformRepo
  {
    private readonly AppDbContext _context;

    public PlatformRepo(AppDbContext context)
    {
      _context = context;
    }

    public void CreatePlatform(Platform plat)
    {
      if (plat == null)
      {
        throw new ArgumentNullException(nameof(plat));
      }
      _context.Platforms.Add(plat);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
      return _context.Platforms.ToList();
    }

    #pragma warning disable CS8766
    public Platform? GetPlatformById(int Id)
    {
      return _context.Platforms.FirstOrDefault(p => p.Id == Id);
    }

    public bool SaveChanges()
    {
      return _context.SaveChanges() >= 0;
    }
  }
}