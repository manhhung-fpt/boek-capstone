using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.CustomerOrganizations
{
    public class OwnedCustomerOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        public CustomerUserViewModel Customer { get; set; }

        public List<CustomerOrganizationsViewModel> Organizations { get; set; }
    }
}
