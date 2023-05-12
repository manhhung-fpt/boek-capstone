using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Author> GetAuthors(int? BookId)
        {
            var authors = new List<Author>();
            var list = dbSet.Where(ba => ba.BookId.Equals(BookId))
                .Include(ba => ba.Author)
                .ToList();
            if (list != null)
                list.ForEach(ba => authors.Add(ba.Author));
            return authors;
        }

        public List<Book> GetBooks(int? authorId)
        {
            var books = new List<Book>();
            var list = dbSet.Where(ba => ba.AuthorId.Equals(authorId))
                .Include(ba => ba.Book)
                .ToList();
            if (list != null)
                list.ForEach(ba => books.Add(ba.Book));
            return books;
        }
    }
}
