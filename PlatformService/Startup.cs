using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlatformService.DATA;
using PlatformService.DataServiceAsync;
using PlatformService.DataServiceSync.Http;
using PlatformService.DataServiceAsync.Grpc;

namespace PlatformService
{
  public class Startup
  {
    public IConfiguration Configuration { get; }
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      _env = env;
    }


    public void ConfigureServices(IServiceCollection services)
    {
      if (_env.IsProduction())
      {
        Console.WriteLine("--> Using SQL DB");
        services.AddDbContext<AppDbContext>(opt =>
          opt.UseSqlServer(Configuration.GetConnectionString("PlatformConn")));
      }
      else
      {
        Console.WriteLine("--> Using InMem DB");
        services.AddDbContext<AppDbContext>(opt =>
           opt.UseInMemoryDatabase("InMem"));
      }

      services.AddScoped<IPlatformRepo, PlatformRepo>();
      services.AddSingleton<IMessageBusClient, MessageBusClient>();
      services.AddGrpc();
      services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

      // services.AddGrpc();
      services.AddControllers();
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "PlatformService", Version = "v1" });
      });
      Console.WriteLine("--> CommadService " + Configuration["CommandService"]);

    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // if (env.IsDevelopment())
      // {
      app.UseDeveloperExceptionPage();
      app.UseSwagger(c =>
      {
      });
      app.UseSwaggerUI(c =>
      {
        // c.RoutePrefix = "swagger/platform";
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1");
      });
      // }

      //app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        // Configure Grpc Routes
        endpoints.MapGrpcService<GrpcPlatformService>();

        endpoints.MapGet("/proto/platforms.proto", async context =>
        {
          await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
        });
      });

      // app.UseEndpoints(endpoints =>
      // {
      //   endpoints.MapControllers();
      //   endpoints.MapGrpcService<GrpcPlatformService>();

      //   endpoints.MapGet("/protos/platforms.proto", async context =>
      //   {
      //     await context.Response.WriteAsync(File.ReadAllText("Protos/platforms.proto"));
      //   });
      // });
      PrepDb.PrepPopulation(app, env.IsProduction());
    }
  }
}