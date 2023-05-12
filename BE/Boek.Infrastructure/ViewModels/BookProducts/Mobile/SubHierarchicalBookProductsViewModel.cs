using Boek.Infrastructure.ViewModels.Genres;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.BookProducts.Mobile
{
    public class SubHierarchicalBookProductsViewModel
    {
        public string SubTitle { get; set; }
        public int? GenreId { get; set; }
        public Guid? IssuerId { get; set; }
        public GenreViewModel Genre { get; set; }
        public UserViewModel Issuer { get; set; }
        public List<MobileBookProductsViewModel> BookProducts { get; set; }
    }
}