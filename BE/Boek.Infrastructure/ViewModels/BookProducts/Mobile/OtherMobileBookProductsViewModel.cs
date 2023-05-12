using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.BookProducts.Mobile
{
    public class OtherMobileBookProductsViewModel
    {
        [Guid]
        public Guid Id { get; set; }
        [Int]
        public int? BookId { get; set; }
        [Int]
        public int? GenreId { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [String]
        public string CampaignName { get; set; }
        [Int]
        public int? Discount { get; set; }
        [Decimal]
        public decimal SalePrice { get; set; }
    }
}