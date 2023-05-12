using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Orders
{
    public class QROrdersRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.USER_ID}")]
        public Guid? CustomerId { get; set; }
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_ID}")]
        public int? CampaignId { get; set; }
        public List<Guid> OrderIds { get; set; }
    }
}