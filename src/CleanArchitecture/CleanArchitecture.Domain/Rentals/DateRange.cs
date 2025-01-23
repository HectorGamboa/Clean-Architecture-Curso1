using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace CleanArchitecture.Domain.Rentals
{
    public sealed record DateRange
    {
        private DateRange(){

        }
        // only stores date
        public DateOnly Start { get; init; }
        public DateOnly End { get; init; }

        public int NumberOfDays => End.DayNumber - Start.DayNumber;

        public static DateRange Create(DateOnly start, DateOnly end)
        {
            if (start > end)
            {
                throw new ApplicationException("The start date must be before the end date.");
            }

            return new DateRange
            {
                Start = start,
                End = end
            };
        }

        
    }
}