using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CustomerGroups
{
    public class CustomerGroupRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        [Int]
        public int? GroupId { get; set; }
    }
}
