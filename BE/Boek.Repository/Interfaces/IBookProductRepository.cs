using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IBookProductRepository : IGenericRepository<BookProduct>
    {
        BookProduct CheckDuplicatedComboName(string title, int campaignId);
    }
}
