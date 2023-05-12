using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Genre CheckDuplicatedGenre(string name);
        bool ValidGenre(int? GenreId);
    }
}
