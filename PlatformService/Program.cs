namespace PlatformService
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
          // webBuilder.UseUrls("http://0.0.0.0:8080");
          webBuilder.UseStartup<Startup>();
        });
  }
}