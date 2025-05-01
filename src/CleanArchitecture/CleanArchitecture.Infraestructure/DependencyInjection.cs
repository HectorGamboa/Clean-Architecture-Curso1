using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Infraestructure.Clock;
using CleanArchitecture.Infraestructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider,DateTimeProvider>();
            services.AddTransient<IEmailService,EmailService>();
            var connectionString = configuration.GetConnectionString("DataBase")?? throw new ArgumentNullException(nameof(configuration));
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            }); 
            return services;
        }
    }
}