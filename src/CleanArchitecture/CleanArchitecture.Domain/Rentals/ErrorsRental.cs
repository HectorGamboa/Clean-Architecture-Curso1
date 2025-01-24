using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals
{
    public static class ErrorsRental
    {
        public static Error NotFound = new Error(
            "Rental.NotFound", 
            "The rental was not found."
        );
        public static Error Overlap = new Error(
            "Rental.Overlap",
            "The rental is being taken by two or more customers at the same time on the same date."
        );

        public static Error NotReserved = new Error(
            "Rental.NotReserved",
            "The rental is not reserved."
        );

        public static Error NotConfirmed = new Error(
            "Rental.NotConfirmed",
            "The rental is not confirmed."
        );

        public static Error AlreadyStarted = new Error(
            "Rental.AlreadyStarted",
            "The rental has already started."
        );
    }
}