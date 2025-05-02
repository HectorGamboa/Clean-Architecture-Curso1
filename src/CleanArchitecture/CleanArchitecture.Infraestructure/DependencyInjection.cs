using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Infraestructure.Clock;
using CleanArchitecture.Infraestructure.Data;
using CleanArchitecture.Infraestructure.Email;
using CleanArchitecture.Infraestructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IVehicleRepository,VehiculeRepository>();
            services.AddScoped<IRentalRepository,RantalRepository>();
            services.AddScoped<IUnitOfWork>(sp=>sp.GetRequiredService<ApplicationDbContext>());
            services.AddSingleton<ISqlConnectionFactory>(_ =>{ return  new SqlConnectionFactory(connectionString);});
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
            return services;
        }
    }
}