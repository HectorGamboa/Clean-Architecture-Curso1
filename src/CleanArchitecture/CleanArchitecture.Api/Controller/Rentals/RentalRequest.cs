namespace CleanArchitecture.Api.Controller.Rentals
{
    public sealed record RentalRequest(
        Guid VehicleId,
        Guid UserId,
        DateOnly StartDate,
        DateOnly EndDate
    );
}