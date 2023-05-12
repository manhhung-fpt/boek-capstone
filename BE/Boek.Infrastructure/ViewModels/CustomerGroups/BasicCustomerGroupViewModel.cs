using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CustomerGroups
{
    public class BasicCustomerGroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        [Int]
        public int? GroupId { get; set; }
    }
}
