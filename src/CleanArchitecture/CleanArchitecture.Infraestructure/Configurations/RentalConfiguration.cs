

using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals");
            builder.HasKey(r => r.Id);
            builder.OwnsOne(rental => rental.CurrencyPerPeriod, precioBuilder =>
            {
                precioBuilder.Property(moneda => moneda.TypeCurrency)
                .HasConversion(typecurrency => typecurrency.Code, code => TypeCurrency.FromCode(code!));
            });
            builder.OwnsOne(rental => rental.Maintenance, precioBuilder =>
            {
                precioBuilder.Property(moneda => moneda.TypeCurrency)
                .HasConversion(typecurrency => typecurrency.Code, code => TypeCurrency.FromCode(code!));
            });
            builder.OwnsOne(rental => rental.Accessories, precioBuilder =>
            {
                precioBuilder.Property(moneda => moneda.TypeCurrency)
                .HasConversion(typecurrency => typecurrency.Code, code => TypeCurrency.FromCode(code!));
            });
            builder.OwnsOne(rental => rental.TotalPrice, precioBuilder =>
            {
                precioBuilder.Property(moneda => moneda.TypeCurrency)
                .HasConversion(typecurrency => typecurrency.Code, code => TypeCurrency.FromCode(code!));
            });
            builder.OwnsOne(rental => rental.DateRange);
            builder.HasOne<Vehicle>().WithMany().HasForeignKey(rental => rental.VehicleId);
            
            builder.HasOne<User>().WithMany().HasForeignKey(aquiler => aquiler.UserId);
        }
    }
}