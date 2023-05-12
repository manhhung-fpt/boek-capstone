using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders.Guest
{
    public class CreateGuestPickUpOrderRequestModel
    {
        public int? CampaignId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Phone]
        public string CustomerPhone { get; set; }
        [EmailAddress, Required]
        public string CustomerEmail { get; set; }
        public byte Payment { get; set; }
        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}