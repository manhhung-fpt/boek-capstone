using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders
{
    public class CreateCustomerPickUpOrderRequestModel
    {
        public int? CampaignId { get; set; }
        public byte Payment { get; set; }
        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }
}