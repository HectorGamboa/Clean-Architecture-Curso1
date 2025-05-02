
using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options){
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}