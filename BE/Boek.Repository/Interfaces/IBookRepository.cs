using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Book CheckDuplicatedBookName(string name);
        List<Campaign> GetCampaigns(int id);
        bool IsAllowChangingGenre(int id);
    }
}
