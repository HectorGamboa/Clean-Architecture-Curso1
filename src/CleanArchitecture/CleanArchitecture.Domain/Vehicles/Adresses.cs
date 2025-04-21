using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Vehicles
{
    public record Adresser
   (
    string Country,
    string Department,
    string Province,
    string City,
    string Street
   );
}