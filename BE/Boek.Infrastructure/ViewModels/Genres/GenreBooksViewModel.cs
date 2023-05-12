using Boek.Core.Entities;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Books;

namespace Boek.Infrastructure.ViewModels.Genres
{
    public class GenreBooksViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? ParentId { get; set; }
        [String]
        public string Name { get; set; }
        [Int]
        public int? DisplayIndex { get; set; }
        [Boolean]
        public bool? Status { get; set; }
        [String]
        public string StatusName { get; set; }
        public List<BasicBookViewModel> Books { get; set; }
    }
}
