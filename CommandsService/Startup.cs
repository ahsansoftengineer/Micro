using CommandsService.AsyncDataServices;
using CommandsService.DATA;
using CommandsService.EventProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CommandsService;
public class Startup
{
    public IConfiguration _config { get; }
    private readonly IWebHostEnvironment _env;

    public Startup(IConfiguration config, IWebHostEnvironment env)
    {
        _config = config;
        _env = env;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemo"));
        services.AddScoped<ICommandRepo, CommandRepo>();

        services.AddControllers();

        services.AddHostedService<MessageBusSubscriber>();
        services.AddSingleton<IEventProcessor, EventProcessor>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // services.AddScoped<IPlatformDataClient, PlatformDataClient>();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PlatformService",
                Version = "v1",
            });
        });
       
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // c.RoutePrefix = "swagger/command";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommandsService v1");
            });
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}