namespace Boek.Infrastructure.Requests.OrderDetails
{
    public class CreateOrderDetailsRequestModel
    {
        public Guid? BookProductId { get; set; }
        public int? Quantity { get; set; }
        public bool? WithPdf { get; set; }
        public bool? WithAudio { get; set; }
    }
}