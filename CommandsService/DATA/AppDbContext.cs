using CommandsService.MODELS;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.DATA 
{
  public class AppDbContext: DbContext 
  {
    public AppDbContext(DbContextOptions opt) : base(opt)
    {
      
    }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
      mb.Entity<Platform>()
      .HasMany(p => p.Commands)
      .WithOne(p => p.Platform!)
      .HasForeignKey(p => p.PlatformId!);

      mb.Entity<Command>()
      .HasOne(p => p.Platform)
      .WithMany(p => p.Commands)  
      .HasForeignKey(p => p.PlatformId);
    }
  }
}