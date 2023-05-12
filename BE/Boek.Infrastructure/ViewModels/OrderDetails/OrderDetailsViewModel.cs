using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.BookProducts;

namespace Boek.Infrastructure.ViewModels.OrderDetails
{
    public class OrderDetailsViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? OrderId { get; set; }
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
        public OrderBookProductsViewModel BookProduct { get; set; }
    }
}