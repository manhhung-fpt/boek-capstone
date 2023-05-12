using Boek.Core.Enums;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Books.Mobile;
using Boek.Infrastructure.ViewModels.BookProductItems.Mobile;

namespace Boek.Infrastructure.Requests.BookProducts.Mobile
{
    public class BookProductMobileTypeRequestModel
    {
        public BookProductMobileTypeRequestModel(BookMobileRequestModel book, BookType type)
        {
            if (book.BookAuthors != null)
                book.BookAuthors = null;
            if (type.Equals(BookType.Odd))
                this.Book = book;
            else
            {
                this.BookProductItems = new BookProductItemMobileRequestModel() { Book = book };
                if (type.Equals(BookType.Series))
                    this.Book = book;
            }
        }
        [ChildRange]
        public BookMobileRequestModel Book { get; set; }
        [ChildRange]
        public BookProductItemMobileRequestModel BookProductItems { get; set; }
    }
}