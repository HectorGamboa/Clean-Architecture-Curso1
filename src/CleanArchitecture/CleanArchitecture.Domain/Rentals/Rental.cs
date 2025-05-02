using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rentals
{
    public sealed class Rental : Entity
    {
        private Rental(
            Guid id,
            Guid userId,
            Guid vehicleId,
            DateRange dateRange,
            Currency currencyPerPeriod,
            Currency maintenance,
            Currency accessories,
            Currency totalPrice,
            StatusRental status,
            DateTime? dateCreated

            ) : base(id)
        {
            UserId = userId;
            VehicleId = vehicleId;
            DateRange = dateRange;
            CurrencyPerPeriod = currencyPerPeriod;
            Maintenance = maintenance;
            Accessories = accessories;
            TotalPrice = totalPrice;
            Status = status;
            DateCreated = dateCreated;
        }
        public Guid UserId { get; private set; }
        public Guid VehicleId { get; private set; }
        public Currency CurrencyPerPeriod { get; private set; }
        public Currency Maintenance { get; private set; }
        public Currency Accessories { get; private set; }
        public Currency TotalPrice { get; private set; }
        public StatusRental Status { get; private set; }
        public DateRange DateRange { get; private set; }
        public DateTime? DateCreated { get; private set; }
        public DateTime? DateConfirmed { get; private set; }
        public DateTime? DateRejected { get; private set; }
        public DateTime? DateCompleted { get; private set; }
        public DateTime? DateCanceled { get; private set; }

        public static Rental Reserve(
            Guid userId,
            Vehicle vehicle,
            DateRange dateRange,
            DateTime dateCreated,
            PriceService priceService
        )
        {
            var priceDetail = priceService.CalculatePrice(vehicle, dateRange);
            var rental = new Rental(
                Guid.NewGuid(),
                userId,
                vehicle.Id,
                dateRange,
                priceDetail.PriceByPeriod,
                priceDetail.Mantainance,
                priceDetail.Accessories,
                priceDetail.PriceTotal,
                StatusRental.Reserved,
                dateCreated
            );

            rental.RaiseDomainEvent(new RentalReservedDomainEvent(rental.Id));
            vehicle.DateOfLastRental = dateCreated;
            return rental;
        }

        public Result  Confirm(DateTime utcNow){
            if(Status !=  StatusRental.Reserved){
                // se dispare una exception con un message error
                 return Result.Failure(ErrorsRental.NotReserved);
            }
            Status = StatusRental.Confirmed;
            DateConfirmed = utcNow;
            RaiseDomainEvent(new RentalConfirmedDomainEvent(Id));
            return Result.Success();
        }

        public  Result Reject(DateTime utcNow){
            if(Status != StatusRental.Reserved){
                // se dispare una exception con un message error
                return Result.Failure(ErrorsRental.NotReserved);
            }
            Status = StatusRental.Rejected;
            DateRejected = utcNow;
            RaiseDomainEvent(new RentalRejectedDomainEvent(Id));
            return Result.Success();
        }

        public  Result Canceled(DateTime utcNow){
            if(Status != StatusRental.Confirmed){
                // se dispare una exception con un message error
                return Result.Failure(ErrorsRental.NotConfirmed);
            }

            var currentDate = DateOnly.FromDateTime(utcNow);
            if(DateRange.Start < currentDate){
                // se dispare una exception con un message error
                return Result.Failure(ErrorsRental.AlreadyStarted);
            }
            Status = StatusRental.Canceled;
            DateCanceled = utcNow;
            RaiseDomainEvent(new RentalCanceledDomainEvent(Id));
            return Result.Success();
        }
   
        public Result Complete(DateTime utcNow){
            if(Status != StatusRental.Confirmed){
                // se dispare una exception con un message error
                return Result.Failure(ErrorsRental.NotConfirmed);
            }
            var currentDate = DateOnly.FromDateTime(utcNow);
            if(DateRange.End < currentDate){
                // se dispare una exception con un message error
                return Result.Failure(ErrorsRental.NotConfirmed);
            }
            Status = StatusRental.Completed;
            DateCompleted = utcNow;
            RaiseDomainEvent(new RentalCompletedDomainEvent( Id));
            return Result.Success();
        }
    }
}