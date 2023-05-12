using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders.Calculation
{
    public class ShippingOrderCalculationRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_ID}")]
        public int? CampaignId { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}