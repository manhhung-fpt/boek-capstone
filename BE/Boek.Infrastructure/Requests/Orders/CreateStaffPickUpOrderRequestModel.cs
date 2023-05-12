using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders
{
    public class CreateStaffPickUpOrderRequestModel
    {
        public Guid? CustomerId { get; set; }
        public int? CampaignId { get; set; }
        [MaxLength(255)]
        public string CustomerName { get; set; }
        [MaxLength(50)]
        public string CustomerPhone { get; set; }
        [MaxLength(255)]
        public string CustomerEmail { get; set; }
        public string Address { get; set; }
        public byte Payment { get; set; }

        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}