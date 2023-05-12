using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders
{
    public class OrderFilterRequestModel
    {
        [GuidRange, Range]
        public List<Guid?> Ids { get; set; }
        [String]
        public string Code { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Guid]
        public Guid? CustomerId { get; set; }
        [DateRange, RangeField]
        public List<DateTime?> OrderDates { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [Byte]
        public byte? Type { get; set; }
        [Sort]
        public string Sort { get; set; }
        [ChildRange]
        public CampaignOrderRequestModel Campaign { get; set; }
        [ChildRange]
        public OrderDetailRequestModel OrderDetails { get; set; }
        [Child]
        public CampaignStaffRequestModel CampaignStaff { get; set; }
    }
}
