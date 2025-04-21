

using CleanArchitecture.Application.Abstractions.Messging;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    public sealed record SearchVehiclesQuery(
        DateOnly? StartDate,
        DateOnly? EndDate
    ):IQuery<IReadOnlyList<VehicleResponse>>;
}