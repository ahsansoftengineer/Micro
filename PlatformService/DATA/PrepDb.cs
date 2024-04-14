using Microsoft.EntityFrameworkCore;
using PlatformService.MODELS;

namespace PlatformService.DATA 
{
  public static class PrepDb 
  {
    public static void PrepPopulation(IApplicationBuilder app, bool isProd) 
    {
      using(var serviceScope = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
      }
    }

    private static void SeedData(AppDbContext context, bool isProd) {
      if(isProd)
      {
        Console.WriteLine("--> Attempting to apply Migrations...");
        try {
          context.Database.Migrate();
        } catch(Exception ex) {
          Console.WriteLine(ex.Message);
        }
      }
      if(!context.Platforms.Any()){
        Console.WriteLine("--> Seeding Data");

        context.Platforms.AddRange(
          new Platform() {
            Name = "Dot Net",
            Publisher="Microsoft",
            Cost="Free"
          },
          new Platform() {
            Name = "Sql Server Express",
            Publisher="Microsoft",
            Cost="Free"
          },
          new Platform() {
            Name = "Kubernetes",
            Publisher="Cloud Native Computing Foundation",
            Cost="Paid"
          }
        );
        context.SaveChanges();

      } else {
        Console.WriteLine("--> We already have data");
      }
    }
  }
}