

using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Rentals.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Rentals.RentalReservation
{
    internal sealed class RentalReservationDomainEventHandler
    : INotificationHandler<RentalReservedDomainEvent>
    {
        
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public RentalReservationDomainEventHandler(
            IRentalRepository rentalRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }


        public async Task Handle(RentalReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(notification.RentalId, cancellationToken);
            if (rental == null)
            {
                return;
            }
            var user = await _userRepository.GetByIdAsync(rental.UserId, cancellationToken);
            if (user == null)
            {
                return;
            }
            await _emailService.SendEmailAsync(user.Email!, "Rental Reserved", "Your rental has been reserved.");
        }
    }
}