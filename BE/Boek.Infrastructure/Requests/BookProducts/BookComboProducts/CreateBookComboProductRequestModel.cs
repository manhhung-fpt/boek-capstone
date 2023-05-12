using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.BookProductItems;

namespace Boek.Infrastructure.Requests.BookProducts.BookComboProducts
{
    public class CreateBookComboProductRequestModel
    {
        public int? GenreId { get; set; }
        public int? CampaignId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public decimal SalePrice { get; set; }
        public int Commission { get; set; }
        public List<CreateBookProductItemRequestModel> BookProductItems { get; set; }
    }
}