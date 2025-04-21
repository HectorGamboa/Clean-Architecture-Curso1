
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using Dapper;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    internal sealed class SearchVehiclesQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private  static  readonly int [] ActiveAlquilerStatuses =
            {
                (int)StatusRental.Reserved,
                (int)StatusRental.Confirmed,
                (int)StatusRental.Completed
            };

        public SearchVehiclesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
        {
            if(request.StartDate > request.EndDate)
            {
                return  new List<VehicleResponse>();
            }
            using var connection =  _sqlConnectionFactory.CreateConnection();
            const string  sql = """ 
                SELECT 
                    a.Id AS Id,
                    a.model AS Model,
                    a.vin AS Vin,
                    a.price_mount AS PriceAmount,
                    a.price_currency AS PriceCurrency,
                    a.adress_country AS Country,
                    a.adress_department AS Department,
                    a.adress_province AS Province,
                    a.adress_city AS City,
                    a.adress_street AS Street,


                FROM Vehicles as  a
                WHERE  NOT EXISTS (
                    SELECT 
                        1 FROM Rentals as b
                    WHERE 
                        a.Id = b.VehicleId AND
                        b.StartDate <= @EndDate AND
                        b.EndDate >= @StartDate AND
                        b.Status = ANY(@ActiveRentailStatuses)

                )
            """;

            var vehicles = await connection.QueryAsync<VehicleResponse,AdresserResponse,VehicleResponse>(
                sql,
                (vehicle, address) =>
                {
                    vehicle.Address = address;
                    return vehicle;
                },
                new
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                     ActiveAlquilerStatuses
                },
                splitOn: "Country"
            );
            
            return vehicles.ToList();
        }
    }
}