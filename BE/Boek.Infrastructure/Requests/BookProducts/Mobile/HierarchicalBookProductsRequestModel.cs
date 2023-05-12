using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;
using Boek.Core.Constants.Mobile;

namespace Boek.Infrastructure.Requests.BookProducts.Mobile
{
    public class HierarchicalBookProductsRequestModel
    {
        public int? CampaignId { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public Guid? IssuerId { get; set; }

        public bool IsNotEmpty()
        => CampaignId.HasValue && !string.IsNullOrEmpty(Title);
    }
}