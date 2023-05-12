using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.BookProducts;

namespace Boek.Infrastructure.Requests.OrderDetails
{
    public class OrderDetailRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? OrderId { get; set; }
        [Guid]
        public Guid? BookProductId { get; set; }
        [Int]
        public int? Quantity { get; set; }
        [Decimal]
        public decimal? Price { get; set; }
        [Int]
        public int? Discount { get; set; }
        [Boolean]
        public bool? WithPdf { get; set; }
        [Boolean]
        public bool? WithAudio { get; set; }
        [ChildRange]
        public BookProductRequestModel BookProduct { get; set; }
    }
}