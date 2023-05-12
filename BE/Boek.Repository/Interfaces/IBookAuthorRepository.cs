
using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IBookAuthorRepository : IGenericRepository<BookAuthor>
    {
        public List<Author> GetAuthors(int? BookId);
        public List<Book> GetBooks(int? authorId);
    }
}
