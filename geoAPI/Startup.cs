using geoAPI.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using geoAPI.App.Repositories.Interfaces;
using geoAPI.App.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace geoAPI
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
    {
        // Set up the database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"),
            b =>
            {
                b.MigrationsAssembly("geoAPI.App");
            })
        );

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = Configuration.GetSection("Identity").GetValue<string>("Authority");

                options.ApiName = "geoapi";
                options.RequireHttpsMetadata = false;
            });

        services.AddControllers();

        // Add repositories to dependency injection
        services.AddScoped<ICompanyRepository, CompanyRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      // Initialize the database
      UpdateDatabase(app);

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    // Update the database to the latest migration
    private static void UpdateDatabase(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
            {
                    context.Database.Migrate();
            }
        }
    }
  }
}
