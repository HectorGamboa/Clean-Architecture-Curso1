using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Infraestructure.Repositories
{
    internal sealed class VehiculeRepository:Repository<Vehicle>, IVehicleRepository
    {
        public VehiculeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}