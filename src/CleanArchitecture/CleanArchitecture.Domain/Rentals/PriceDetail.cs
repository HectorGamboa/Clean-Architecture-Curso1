using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rentals
{
    public record PriceDetail(
        Currency PriceByPeriod,
        Currency Mantainance,
        Currency Accessories,
        Currency  PriceTotal
    );
}