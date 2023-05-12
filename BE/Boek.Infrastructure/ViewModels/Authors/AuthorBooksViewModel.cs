using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Books;

namespace Boek.Infrastructure.ViewModels.Authors
{
    public class AuthorBooksViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string ImageUrl { get; set; }
        [String]
        public string Description { get; set; } 

        public List<BasicBookViewModel> Books { get; set; }
    }
}
