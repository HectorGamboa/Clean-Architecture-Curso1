using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rentals.Events
{
    public sealed record RentalCanceledDomainEvent(Guid RentalId): IDomainEvent;
}