using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Books.Mobile;

namespace Boek.Infrastructure.ViewModels.BookProductItems.Mobile
{
    public class BookProductItemMobileRequestModel
    {
        [ChildRange]
        public BookMobileRequestModel Book { get; set; }
    }
}