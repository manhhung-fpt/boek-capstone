using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Authors
{
    public class AuthorViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string ImageUrl { get; set; }
        [String]
        public string Description { get; set; }
    }
}
