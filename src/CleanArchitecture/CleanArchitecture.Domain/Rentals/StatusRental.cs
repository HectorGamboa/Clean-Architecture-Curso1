using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Rentals
{
    public enum StatusRental
    {
        Reserved = 1,
        Confirmed = 2,
        Rejected = 3,
        Canceled = 4,
        Completed = 5
    }
}