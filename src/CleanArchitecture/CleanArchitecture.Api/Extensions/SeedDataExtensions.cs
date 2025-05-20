using Bogus;
using Bogus.DataSets;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Vehicles;

(

namespace CleanArchitecture.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var sqlConnection = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnection.CreateConnection();

            var faker = new Faker();
            List<object> vehicles = [];

            for (var i = 0; i < 100; i++)
            {
                vehicles.Add(new
                {
                    Guid = Guid.NewGuid(),
                    Vin = faker.Vehicle.Vin(),
                    Model = faker.Vehicle.Model(),
                    Country = faker.Address.Country(),
                    Department = faker.Address.State(),
                    Province = faker.Address.County(),
                    City = faker.Address.City(),
                    Street = faker.Address.StreetAddress(),
                    Price = faker.Random.Decimal(1000, 10000),
                    Currency = "USD",
                    MaintenancePrice = faker.Random.Decimal(100, 1000),
                    MaintenanceCurrency = "USD",
                    Accessories = new List<int>
                    {
                       (int) Accessory.Wifi,
                       (int) Accessory.AppleCar,

                    },
                    DateOfLastRental = DateTime.MinValue,
                });
            }

            const string sql = """
                INSERT INTO public.vehicles
                (id, vin, model, price,di, currency, maintenance_price, maintenance_currency, date_of_last_rental)
            """;
        }
    }
        
}
