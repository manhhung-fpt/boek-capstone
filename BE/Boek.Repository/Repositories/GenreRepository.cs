using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public Genre CheckDuplicatedGenre(string name) => dbSet
            .SingleOrDefault(g => g.Name.ToLower().Trim().Equals(name.ToLower().Trim()));

        public bool ValidGenre(int? GenreId) => dbSet.Any(g => g.Id.Equals(GenreId));
    }
}
