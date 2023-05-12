using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Authors;

namespace Boek.Infrastructure.ViewModels.BookAuthors
{
    public class BookAuthorViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? BookId { get; set; }
        [Int]
        public int? AuthorId { get; set; }

        public AuthorViewModel Author { get; set; }
    }
}
