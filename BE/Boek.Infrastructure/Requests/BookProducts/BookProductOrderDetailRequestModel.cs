using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.BookProducts
{
    public class BookProductOrderDetailRequestModel
    {
        [Guid]
        public Guid? IssuerId { get; set; }
    }
}