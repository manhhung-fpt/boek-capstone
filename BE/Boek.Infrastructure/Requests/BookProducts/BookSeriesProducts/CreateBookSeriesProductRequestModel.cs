using Boek.Infrastructure.Requests.BookProductItems;

namespace Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts
{
    public class CreateBookSeriesProductRequestModel
    {
        public int? BookId { get; set; }
        public int? CampaignId { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
        public List<CreateBookProductItemRequestModel> BookProductItems { get; set; }
    }
}