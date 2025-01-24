using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Vehicles;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals
{
    public class PriceService
    {
        public  PriceDetail CalculatePrice(Vehicle vehicle, DateRange dateRange)
        {
          var typeCurrency= vehicle.Price!.TypeCurrency;
          var priceByPeriod = new Currency(dateRange.NumberOfDays*vehicle.Price.Amount, typeCurrency);
          decimal porcentageChange=0;
          foreach (var accessory in vehicle.Accessories)
          {
              porcentageChange+=accessory  switch{
                    Accessory.AppleCar or Accessory.AndroidCar=>0.05m,
                    Accessory.AirConditioning =>0.01m,
                    Accessory.Maps=>0.01m,
                    _=>0
              };
          }
          var accessorieCharges = Currency.Zero(typeCurrency);
          if (porcentageChange>0)
            {
                accessorieCharges = new Currency(priceByPeriod.Amount*porcentageChange, typeCurrency);
            }
            var  priceTotal = Currency.Zero();
            priceTotal += priceByPeriod ;
            priceTotal += accessorieCharges;
            if (vehicle.MaintenanceCurrency != null)
            {
                priceTotal += vehicle.MaintenanceCurrency;
            }

            return new PriceDetail(priceByPeriod, 
            vehicle.MaintenanceCurrency ?? Currency.Zero(), 
            accessorieCharges, 
            priceTotal);
        }


    }
}