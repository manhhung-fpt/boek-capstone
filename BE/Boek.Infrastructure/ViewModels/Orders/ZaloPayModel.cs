using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.ViewModels.Orders
{
    public class ZaloPayCreateOrder
    {
        public string User { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [Required]
        public string RedirectUrl { get; set; }
        public IEnumerable<ZaloPayItemParams> Items { get; set; }
        public IEnumerable<Guid> OrderIds { get; set; }
    }

    public class ZaloPayItemParams
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public uint ItemQuantity { get; set; }
    }
    public class ZaloPayOrderQueryModel
    {
        public string AppTransId { get; set; }
    }
}
