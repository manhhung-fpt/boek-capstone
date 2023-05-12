using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.OrderDetails;

namespace Boek.Infrastructure.Requests.Orders.ZaloPay
{
    public class CreateZaloPayOrderRequestModel
    {
        public int? CampaignId { get; set; }
        public Guid? CustomerId { get; set; }
        [MaxLength(255)]
        public string CustomerName { get; set; }
        [MaxLength(50)]
        public string CustomerPhone { get; set; }
        [MaxLength(255)]
        public string CustomerEmail { get; set; }
        public byte Type { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        public string Address { get; set; }
        public decimal? Freight { get; set; }
        public byte Payment { get; set; }
        public string Description { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
        public List<CreateOrderDetailsRequestModel> OrderDetails { get; set; }
    }

    public class ZaloPayCallBackRequestModel
    {
        public int app_id { get; set; }
        public string app_trans_id { get; set; }
        public long app_time { get; set; }
        public string app_user { get; set; }
        public int amount { get; set; }
        public string embed_data { get; set; }
        public string item { get; set; }
        public long zp_trans_id { get; set; }
        public long server_time { get; set; }
        public int channel { get; set; }
        public string merchant_user_id { get; set; }
        public int user_fee_amount { get; set; }
        public int discount_amount { get; set; }
    }

    public class EmbedDataRequestModel
    {
        public string merchantinfo { get; set; }
        public string promoptioninfo { get; set; }
        public string OrderIds { get; set; }
    }

    public class EmbedDataQrequestCreateModel
    {
        public string redirecturl { get; set; }
        public IEnumerable<Guid> OrderIds { get; set; }
    }

    public class FirstHierarchyZaloPayCallBackRequestModel
    {
        public string data { get; set; }
        public string mac { get; set; }
        public int type { get; set; }
    }

    public class EmbedDataZaloPayRequestModel
    {
        public string redirecturl { get; set; }
        public List<string> OrderIds { get; set; }
    }
}