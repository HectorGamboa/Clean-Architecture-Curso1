using System.Threading.Tasks;
using CleanArchitecture.Infraestructure;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<ApplicationDbContext>();
                    await context.Database.MigrateAsync();


                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "Error en migracion");
                    
                }
            }
        }        
    }
}