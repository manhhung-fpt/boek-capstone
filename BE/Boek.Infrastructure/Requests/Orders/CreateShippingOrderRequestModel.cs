using System.Text.Json.Serialization;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders
{
    public class CreateShippingOrderRequestModel
    {
        public int? CampaignId { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        public decimal? Freight { get; set; }
        public byte Payment { get; set; }

        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}