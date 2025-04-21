using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;


namespace CleanArchitecture.Domain.Vehicles
{
    public sealed class Vehicle:Entity
    {
        public Vehicle (
            Guid id,
            Model model,
            Vin vin,
            Currency currencyPrice,
            Currency currencyMaintenance,
            DateTime ?dateOfLastRental,
            Adresser addresses,
            List<Accessory> accessories
            ):base(id)
        {
            Model = model;
            Vin = vin;
            Price = currencyPrice;
            MaintenanceCurrency = currencyMaintenance;
            DateOfLastRental = dateOfLastRental ?? DateTime.MinValue;
            Addresses = addresses;
            Accessories = accessories;
        }
        public Model ?Model { get; private set; }
        public Vin ?Vin { get; private set; }
        public Adresser ?Addresses { get; private set; }
        public Currency? Price{ get; private set; }
        public Currency? MaintenanceCurrency{ get; private set; }
        public DateTime DateOfLastRental  { get; internal set; }
        public List<Accessory> Accessories { get; private set; } = [];
    }
}