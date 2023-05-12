using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders.Calculation
{
    public class PickUpOrderCalculationRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_ID}")]
        public int? CampaignId { get; set; }
        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}