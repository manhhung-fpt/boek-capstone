using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.CustomerGroups
{
    public class OwnedCustomerGroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        public CustomerUserViewModel Customer { get; set; }

        public List<CustomerGroupsViewModel> Groups { get; set; }
    }
}
