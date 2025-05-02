using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infraestructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}