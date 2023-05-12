using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders
{
    public class OrderRequestModel
    {
        [GuidRange, Range]
        public List<Guid?> Ids { get; set; }
        [String]
        public string Code { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [DateRange, RangeField]
        public List<DateTime?> OrderDates { get; set; }
        [Byte]
        public byte? Type { get; set; }
        [Sort]
        public string Sort { get; set; }
        [ChildRange]
        public CampaignOrderRequestModel Campaign { get; set; }
        [ChildRange]
        public OrderDetailsRequestModel OrderDetails { get; set; }
    }

    public class OrderOrderDetailCreateRequestModel
    {
        public Guid BookProductId { get; set; }
        public int Quantity { get; set; }
    }
}
