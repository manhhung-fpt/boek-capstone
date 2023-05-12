using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.OrderDetails
{
    public class BasicOrderDetailsViewModel
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
    }
}