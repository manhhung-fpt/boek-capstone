using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class BookProductItemRepository : GenericRepository<BookProductItem>, IBookProductItemRepository
    {
        public BookProductItemRepository(BoekCapstoneContext context) : base(context)
        {
        }
    }
}
