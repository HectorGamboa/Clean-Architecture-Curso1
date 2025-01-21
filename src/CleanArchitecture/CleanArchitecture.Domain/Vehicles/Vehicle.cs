using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Vehiculos
{
    public sealed class Vehiculo:Entity
    {
        public Vehiculo (
            Guid id,
            Model model,
            Vin vin,
            Currency currencyPrice,
            Currency currencyMaintenance,
            DateTime ?dateOfLastRental,
            Addresses addresses,
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
        public Addresses ?Addresses { get; private set; }
        public Currency? Price{ get; private set; }
        public Currency? MaintenanceCurrency{ get; private set; }
        public DateTime DateOfLastRental  { get; private set; }
        public List<Accessory> Accessories { get; private set; } = [];
    }
}