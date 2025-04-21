using System.Data.SqlTypes;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Rentals.GetRental
{
    internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetRentalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<Result<RentalResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();
            var sql = """
                        SELECT 
                            id AS Id,
                            vehiculo_id AS VehicleId,
                            user_id AS UserId,
                            status AS Status,
                            rental_price AS RentalPrice,
                            rental_currency AS RentalCurrency,
                            maintenance_price AS MaintenancePrice,
                            maintenance_currency AS MaintenanceCurrency,
                            accessories_price AS AccessoriesPrice,
                            accessories_currency AS AccessoriesCurrency,
                            total_price AS TotalPrice,
                            total_currency AS TotalCurrency,
                            start_date AS StartDate,
                            end_date AS EndDate,
                            create_date AS CreateDate
                        FROM Rentals WHERE Id = @Id
                        """;
            var rental   = await connection.QueryFirstOrDefaultAsync<RentalResponse>(
               sql,
                new { Id = request.RentalId }
            );
            return rental!;
        }
    }
}
