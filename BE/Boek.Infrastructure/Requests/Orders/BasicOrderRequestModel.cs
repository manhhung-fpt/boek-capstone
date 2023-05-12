using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Orders
{
    public class BasicOrderRequestModel
    {
        public Guid? CustomerId { get; set; }
        public int? CampaignId { get; set; }
        public int? CampaignStaffId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Address { get; set; }
        public decimal? Freight { get; set; }
        public byte Payment { get; set; }
        public byte? Type { get; set; }
        public string Note { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? AvailableDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? CancelledDate { get; set; }
        public byte Status { get; set; }
    }
}