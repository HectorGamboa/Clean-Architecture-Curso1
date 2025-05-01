
using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        
        public ApplicationDbContext(DbContextOptions options) : base(options){
            
        }
    }
}