
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal sealed class VehiculesConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicules");
            builder.HasKey(vehicle => vehicle.Id);
            builder.OwnsOne(vehicle => vehicle.Addresses);
            builder.Property(vehicle =>  vehicle.Model).HasMaxLength(200)
            .HasConversion(model => model!.Value, value => new Model(value));
            builder.Property(vehicle => vehicle.Vin).HasMaxLength(500).HasConversion(
                vin => vin!.Value,
                value => new Vin(value)
            );
            builder.OwnsOne(vehicle => vehicle.Price, priceBuilder =>
            {
                priceBuilder.Property(currency => currency.TypeCurrency)
                .HasConversion(typeCurrency=>typeCurrency.Code, code => TypeCurrency.FromCode(code!));
            });
            builder.OwnsOne(vehicle => vehicle.MaintenanceCurrency, priceBuilder => {
                priceBuilder.Property(currency => currency.TypeCurrency)
                .HasConversion(typeCurrency => typeCurrency.Code, code => TypeCurrency.FromCode(code!));
            });

            builder.Property<uint>("Vesion").IsRowVersion();
        }
    }
}