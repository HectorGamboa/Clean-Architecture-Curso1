using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories
{
    internal sealed class RantalRepository : Repository<Rental>, IRentalRepository
    {
        private static  readonly StatusRental [] statusesAlquiler = 
        { StatusRental.Reserved, StatusRental.Confirmed,StatusRental.Completed };
        public RantalRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange dateRange, CancellationToken cancellationToken = default)
        {
           return await dbContext.Set<Rental>()
                .AnyAsync(
                    r => r.VehicleId == vehicle.Id &&
                     r.DateRange.Start<=dateRange.End &&
                        r.DateRange.End >= dateRange.Start &&
                        statusesAlquiler.Contains(r.Status),
                    cancellationToken
                     );
        }
    }
}