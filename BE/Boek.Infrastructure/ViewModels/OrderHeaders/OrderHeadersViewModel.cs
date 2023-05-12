using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.OrderHeaders
{
    public class OrderHeadersViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? OrderId { get; set; }
        [String]
        public string CustomerName { get; set; }
        [String]
        public string CustomerPhone { get; set; }
        [String]
        public string CustomerEmail { get; set; }
    }
}