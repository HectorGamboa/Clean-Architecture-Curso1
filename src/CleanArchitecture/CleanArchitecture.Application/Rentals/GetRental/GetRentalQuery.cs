using CleanArchitecture.Application.Abstractions.Messging;

namespace CleanArchitecture.Application.Rentals.GetRental
{
    public sealed record GetRentalQuery(Guid RentalId): IQuery<RentalResponse>
    {

    }
}
