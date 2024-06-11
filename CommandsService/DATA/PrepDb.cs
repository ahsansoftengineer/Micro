using CommandsService.DataServicesSync.Grpc;
using CommandsService.MODELS;

namespace CommandsService.DATA
{
  public class PrepDb
  {
    public static void PrepPopulation(IApplicationBuilder builder)
    {
      using (var serviceScope = builder.ApplicationServices.CreateScope())
      {
        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();
        var platforms = grpcClient.ReturnAllPlatforms();
        ICommandRepo? repo = serviceScope.ServiceProvider.GetService<ICommandRepo>();
        // SeedData(repo, platforms);

      }
    }

    private static void SeedData(ICommandRepo repo, IEnumerable<Platform> platforms)
    {
      Console.WriteLine("Seeding new platforms");
      foreach (var plat in platforms)
      {
        if (!repo.ExternalPlatformExist(plat.ExternalId))
        {
          repo.CreatePlatform(plat);
        }
        repo.SaveChanges();
      }
    }
  }
}