using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.BookAuthors.Mobile
{
    public class BookAuthorRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> AuthorIds { get; set; }
    }
}