using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews
{
    public static class ErrorsReview
    {
        public static readonly Error ReviewNotEligibleToReview = 
        new Error("Review.NotElegible", "This vehicle review and rating is not possible if the rental has not been completed.");
    }
}