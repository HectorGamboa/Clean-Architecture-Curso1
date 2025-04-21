
using CleanArchitecture.Application.Abstractions.Behaviors;
using CleanArchitecture.Domain.Rentals;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>{
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                configuration.AddOpenBehavior(typeof(LogginBehaviors<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddTransient<PriceService>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}