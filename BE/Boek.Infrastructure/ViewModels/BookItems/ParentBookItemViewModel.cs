using Boek.Infrastructure.Attributes;
namespace Boek.Infrastructure.ViewModels.Books.BookItems
{
    public class ParentBookItemViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? ParentBookId { get; set; }
        [Int]
        public int? BookId { get; set; }
        [Int]
        public int? DisplayIndex { get; set; }

        public BasicBookViewModel Book { get; set; }
    }
}
