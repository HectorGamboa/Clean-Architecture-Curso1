
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;
        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try{
                 var result = await base.SaveChangesAsync(cancellationToken);
                await PublishDomainEventsAsync();
                return result;
                }catch (DbUpdateConcurrencyException ex){
                    throw new ConcurrencyException("Concurrency error occurred", ex);
                }
           
        }

        private async Task PublishDomainEventsAsync()
        {
            var domains = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }
                ).ToList();

            foreach (var domainEvent in domains)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}