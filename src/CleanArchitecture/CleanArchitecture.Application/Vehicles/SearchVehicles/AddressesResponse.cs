using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles
{
    public sealed class AdresserResponse{
        public string?  Country { get; set; }
        public string? Apartament { get; set; }
        public string? Province { get; set; }
        public string? Street { get; set; }
    }
}