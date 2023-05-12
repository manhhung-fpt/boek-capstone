using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.BookAuthors.Mobile;

namespace Boek.Infrastructure.Requests.Books.Mobile
{
    public class BookMobileRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> GenreIds { get; set; }
        [Range, StringRange, SkipRange, SkipEmptyRange]
        public List<string> Languages { get; set; }
        [Range, GuidRange, SkipRange]
        public List<Guid?> IssuerIds { get; set; }
        [Range, IntRange, SkipRange, SkipEmptyRange]
        public List<int?> PublisherIds { get; set; }
        [ChildRange]
        public BookAuthorRequestModel BookAuthors { get; set; }

        public bool NotNullAuthors() => this.BookAuthors != null;
    }
}