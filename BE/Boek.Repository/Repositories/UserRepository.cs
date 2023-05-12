using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public bool CheckDuplicatedEmail(string email) => dbSet.Any(u =>
            u.Email.ToLower().Trim().Equals(email.ToLower().Trim()));
    }
}
