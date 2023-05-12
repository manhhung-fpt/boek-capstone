using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders.Guest
{
    public class CreateGuestShippingOrderRequestModel
    {
        public int? CampaignId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; }
        [Phone]
        [MaxLength(50)]
        public string CustomerPhone { get; set; }
        [EmailAddress, Required]
        [MaxLength(255)]
        public string CustomerEmail { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        public decimal? Freight { get; set; }
        public byte Payment { get; set; }

        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}