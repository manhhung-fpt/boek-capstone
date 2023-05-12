using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.BookProducts;

namespace Boek.Infrastructure.Requests.OrderDetails
{
    public class OrderDetailsRequestModel
    {
        [ChildRange]
        public BookProductOrderDetailRequestModel BookProduct { get; set; }
    }
}