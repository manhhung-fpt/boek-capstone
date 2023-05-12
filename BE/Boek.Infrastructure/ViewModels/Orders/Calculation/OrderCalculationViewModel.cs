using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.OrderDetails.Calculation;

namespace Boek.Infrastructure.ViewModels.Orders.Calculation
{
    public class OrderCalculationViewModel
    {
        [Decimal]
        public decimal? SubTotal { get; set; }
        [Decimal]
        public decimal? Freight { get; set; }
        [Decimal]
        public decimal? DiscountTotal { get; set; }
        [Decimal]
        public decimal? Total { get; set; }
        [String]
        public string FreightName { get; set; }
        [Decimal]
        public decimal? PlusPoint { get; set; }
        public List<OrderDetailsCalculationViewModel> OrderDetails { get; set; }
    }
}