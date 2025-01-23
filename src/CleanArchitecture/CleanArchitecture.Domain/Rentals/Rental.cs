using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals.Events;
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
            Guid vehicleId,
            DateRange dateRange,
            DateTime? dateCreated,
            PriceDetail priceDetail
        )
        {
            var rental = new Rental(
                Guid.NewGuid(),
                userId,
                vehicleId,
                dateRange,
                priceDetail.PriceByPeriod,
                priceDetail.Mantainance,
                priceDetail.Accessories,
                priceDetail.PriceTotal,
                StatusRental.Reserved,
                dateCreated
            );
            
            rental.RaiseDomainEvent(new RentalReservedDomainEvent(rental.Id));
            return rental;
        }

    }
}