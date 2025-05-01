
using CleanArchitecture.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CleanArchitecture.Infraestructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(user => user.Name)
            .HasMaxLength(200)
            .HasConversion(name => 
            name!.Value, name => new Name(name));

            builder.Property(user => user.LastName).
            HasMaxLength(200).
            HasConversion(appellido => appellido!.Value,  lastName => new LastName(lastName));

            builder.Property(user => user.Email).
            HasMaxLength(400).
            HasConversion(email => email!.Value, email => new Domain.Users.Email(email));

            builder.HasIndex(user => user.Email).
            IsUnique(true);
        }
    }
}