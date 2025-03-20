
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.Rentals.RentalReservation
{
    internal sealed class RentalReservationCommandHandler : ICommandHandler<RentalReservationCommand, Guid>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserRepository _userRepository;
        private readonly PriceService _priceService;
        private readonly IUnitOfWork _unitOfWork;
        private  readonly IDateTimeProvider _dateTimeProvider;

        public RentalReservationCommandHandler(
            IRentalRepository rentalRepository, 
            IVehicleRepository vehicleRepository, 
            IUserRepository userRepository,
             PriceService priceService, 
             IUnitOfWork unitOfWork,
             IDateTimeProvider dateTimeProvider)
        {
            _rentalRepository = rentalRepository;
            _vehicleRepository = vehicleRepository;
            _userRepository = userRepository;
            _priceService = priceService;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(RentalReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }
             var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle is null)
            {
                return Result.Failure<Guid>(VehicleErrors.NotFound);
            }
            
            var duration = DateRange.Create(request.StartDate, request.EndDate);
            if(await _rentalRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
            {
                return Result.Failure<Guid>(ErrorsRental.Overlap);
            }
            var rental = Rental.Reserve( user.Id,vehicle,  duration,_dateTimeProvider.DateTimeCurrenTime,_priceService);
            _rentalRepository.Add(rental);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return rental.Id;
        }
    }

}
