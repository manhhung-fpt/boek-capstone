using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Genres
{
    public class GenreRequestModel
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
        [Skip]
        public bool? IsParentGenre { get; set; } = null;
    }
}
