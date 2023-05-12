using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Author CheckDuplicatedAuthorName(string name);
    }
}
