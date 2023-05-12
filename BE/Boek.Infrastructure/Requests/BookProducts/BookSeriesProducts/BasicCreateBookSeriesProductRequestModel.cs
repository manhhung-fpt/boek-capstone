namespace Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts
{
    public class BasicCreateBookSeriesProductRequestModel
    {
        public int? BookId { get; set; }
        public int? CampaignId { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
    }
}