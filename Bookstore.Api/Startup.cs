using System.Text;
using Bookstore.Api.Mappings;
using Bookstore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Bookstore.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // Dependency Injection services will be configured here
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Automapper implementation
        services.AddAutoMapper(typeof(MappingProfile));
        
        // CORS implementation
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontendClient", builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000") // Only allow requests from this origin
                    .AllowAnyMethod() // Allow all HTTP methods
                    .AllowAnyHeader(); // Allow special headers like Authorization
            });
        });
        
        // DB connection
        services.AddDbContext<BookstoreDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Swagger implementation
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Bookstore API",
                Version = "v1"
            });
        });
    }

    // HTTP Request Pipeline will be managed here
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("AllowFrontendClient"); // CORS must be activated between UseRouting and UseAuthorization

        app.UseAuthentication(); // TODO : JWT Authentication will be activated here
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}