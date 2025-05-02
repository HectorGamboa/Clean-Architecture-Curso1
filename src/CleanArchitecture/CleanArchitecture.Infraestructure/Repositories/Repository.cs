

using CleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infraestructure.Repositories
{
   internal abstract class Repository<T> where T : Entity
    {
        protected readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
         
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id,cancellationToken);
        }
        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }
        

    }
}