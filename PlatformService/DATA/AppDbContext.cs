using Microsoft.EntityFrameworkCore;
using PlatformService.MODELS;

namespace PlatformService.DATA 
{
  public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base (opt) 
    {

    }

    public DbSet<Platform> Platforms { get; set; }
    
  }
}