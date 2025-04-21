
using FluentValidation;

namespace CleanArchitecture.Application.Rentals.RentalReservation
{
    public class RentalAlquilerCommandValidator : AbstractValidator<RentalReservationCommand>
    {
        public RentalAlquilerCommandValidator()
        {
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("Start date must be less than end date.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.VehicleId).NotEmpty().WithMessage("Rental ID is required.");

        }
    }
}