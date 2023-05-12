using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Orders;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.OrderDetails;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.Orders
{
    public class OrderViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [String]
        public string Code { get; set; }
        [Guid]
        public Guid? CustomerId { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? CampaignStaffId { get; set; }
        [String]
        public string CustomerName { get; set; }
        [String]
        public string CustomerPhone { get; set; }
        [String]
        public string CustomerEmail { get; set; }
        //TO-DO: Address Detail
        [String]
        public string Address { get; set; }
        [Decimal]
        public decimal? Freight { get; set; }
        [Byte]
        public byte? Payment { get; set; }
        [String]
        public string PaymentName { get; set; }
        [Byte]
        public byte? Type { get; set; }
        [String]
        public string TypeName { get; set; }
        [String]
        public string Note { get; set; }
        [DateRange]
        public DateTime? OrderDate { get; set; }
        [DateRange, RangeFieldAttribute, JsonIgnore]
        public List<DateTime?> OrderDates { get; set; }
        [DateRange]
        public DateTime? AvailableDate { get; set; }
        [DateRange]
        public DateTime? ShippingDate { get; set; }
        [DateRange]
        public DateTime? ShippedDate { get; set; }
        [DateRange]
        public DateTime? ReceivedDate { get; set; }
        [DateRange]
        public DateTime? CancelledDate { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [String]
        public string StatusName { get; set; }
        [Decimal]
        public decimal? Total { get; set; }
        [Decimal]
        public decimal? SubTotal { get; set; }
        [Decimal]
        public decimal? DiscountTotal { get; set; }
        [String]
        public string FreightName { get; set; }
        [Sort, JsonIgnore]
        public string Sort { get; set; }
        [ChildRange]
        public BasicCampaignViewModel Campaign { get; set; }
        public OrderCampaignStaffViewModel CampaignStaff { get; set; }
        public CustomerUserViewModel Customer { get; set; }
        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
    public class OrderCreateModel
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public int CampaignId { get; set; }
        public byte Payment { get; set; }
        public string Address { get; set; }
        public byte? Type { get; set; }
        public string Note { get; set; }
        public IEnumerable<OrderOrderDetailCreateRequestModel> Products { get; set; }
    }

    public class OrderCreateModelIncludeHeader
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int CampaignId { get; set; }
        public byte? Status { get; set; }
        public string Payment { get; set; }
        public string Address { get; set; }
        public byte? Type { get; set; }
        public string Note { get; set; }
        public IEnumerable<OrderOrderDetailCreateRequestModel> Products { get; set; }

        public bool IsNotEmptyCustomerInfo()
            => !string.IsNullOrEmpty(this.CustomerName) && !string.IsNullOrEmpty(this.CustomerEmail) && !string.IsNullOrEmpty(this.CustomerPhone);
    }

    public class OrderUpdateModel
    {
        public Guid? Id { get; set; }
        public string Note { get; set; }
    }
}
