using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Books.Mobile;

namespace Boek.Infrastructure.Requests.BookProducts.Mobile
{
    public class BookProductMobileRequestModel
    {
        [String]
        public string Title { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Range, ByteRange]
        public List<byte?> Formats { get; set; }
        [Sort]
        public string Sort { get; set; }
        [ChildRange]
        public BookMobileRequestModel Book { get; set; }
        [Skip]
        public HierarchicalBookProductsRequestModel hierarchicalBook { get; set; }
        [Skip]
        public UnhierarchicalBookProductsRequestModel unhierarchicalBook { get; set; }

        public void ClearData()
        {
            this.Title = null;
            this.Formats = null;
            this.Sort = null;
        }
    }
}