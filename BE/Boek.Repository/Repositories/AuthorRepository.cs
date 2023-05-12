using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public Author CheckDuplicatedAuthorName(string name) => dbSet.SingleOrDefault(u =>
            u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }
}
