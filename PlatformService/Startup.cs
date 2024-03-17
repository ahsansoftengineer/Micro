using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using PlatformService.Data;

namespace PlatformService 
{
  public class Startup 
  {
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration) 
    { 
      Configuration = configuration;
    }

    public void ConfigureServicies(IServiceCollection services) {
      services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
      
      services.AddControllers();
      services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new OpenApiInfo {
          Title = "PlatformService"
        });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment())
      {
          app.UseSwagger();
          app.UseSwaggerUI();
      }
    }

  }
}