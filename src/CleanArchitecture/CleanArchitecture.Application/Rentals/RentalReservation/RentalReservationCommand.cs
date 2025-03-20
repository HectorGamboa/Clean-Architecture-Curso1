using CleanArchitecture.Application.Abstractions.Messging;

namespace CleanArchitecture.Application.Rentals.RentalReservation
{
    public record RentalReservationCommand(
        Guid UserId,
        Guid VehicleId,
        DateOnly StartDate,
        DateOnly EndDate
    ):ICommand<Guid>;
}