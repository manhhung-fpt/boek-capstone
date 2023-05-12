using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.OrderDetails.Calculation
{
    public class OrderDetailsCalculationViewModel
    {
        [Guid]
        public Guid? BookProductId { get; set; }
        [Int]
        public int Quantity { get; set; }
        [Decimal]
        public decimal? Price { get; set; }
        [Int]
        public int? Discount { get; set; }
        [Boolean]
        public bool? WithPdf { get; set; }
        [Boolean]
        public bool? WithAudio { get; set; }
        [Decimal]
        public decimal? Total { get; set; }
        [Decimal]
        public decimal? SubTotal { get; set; }
    }
}