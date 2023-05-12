using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.BookProducts.Mobile
{
    public class BasicBookProductMobileRequestModel
    {
        [String]
        public string Title { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Range, ByteRange]
        public List<byte?> Formats { get; set; }
        [Range, IntRange]
        public List<int?> GenreIds { get; set; }
        [Sort]
        public string Sort { get; set; }
    }
}