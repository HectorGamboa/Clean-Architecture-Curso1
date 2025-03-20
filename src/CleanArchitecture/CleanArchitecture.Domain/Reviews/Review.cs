using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rentals;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews
{
    public sealed class Review:Entity
    {
        private Review(Guid id, Guid vehicleId, Guid rentalId, Guid userId, Rating rating, Comment comment):base(id)
        {
            Id = id;
            VehicleId = vehicleId;
            RentalId = rentalId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            DateCreate = DateTime.Now;
        }
        public Guid VehicleId { get; private set; }
        public Guid RentalId { get; private set; }
        public Guid UserId { get; private set; }
        public Rating Rating { get; private set; }
        public Comment Comment { get; private set; }
        public DateTime DateCreate { get; private set; }

        public static Result<Review> Create(Guid vehicleId, Rental rental, Rating rating, Comment comment)
        {
            if( rental.Status != StatusRental.Completed)
            {
                return  Result.Failure<Review>(ErrorsReview.ReviewNotEligibleToReview);
            }
            var review = new Review(Guid.NewGuid(),vehicleId,rental.Id,rental.UserId,rating,comment);
            review.RaiseDomainEvent( new ReviewCreatedDomainEvent(review.Id));
            return review;
        }
        
    }
}