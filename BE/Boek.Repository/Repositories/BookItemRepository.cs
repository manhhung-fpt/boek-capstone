using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class BookItemRepository : GenericRepository<BookItem>, IBookItemRepository
    {
        public BookItemRepository(BoekCapstoneContext context) : base(context)
        {
        }
    }
}
