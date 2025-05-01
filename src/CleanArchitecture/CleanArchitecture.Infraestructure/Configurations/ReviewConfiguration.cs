using CleanArchitecture.Domain.Reviews;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infraestructure.Configurations
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
          builder.ToTable("reviews");
          builder.HasKey(review => review.Id);
          builder.Property(review => review.Rating).
          HasConversion(rating => rating.Value, value => Rating.Create(value!).Value);
          builder.Property(review => review.Comment)
          .HasConversion(comment => comment.Value, value => new Comment(value!));
          builder.HasOne<Vehicle>()
          .WithMany()
          .HasForeignKey(review  => review.VehicleId);

          builder.HasOne<Review>()
          .WithMany()
          .HasForeignKey(review => review.UserId);

          builder.HasOne<User>()
          .WithMany()
          .HasForeignKey(review => review.UserId);
        }
    }
}