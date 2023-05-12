using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.CustomerOrganizations
{
    public class CustomerOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        [Int]
        public int? OrganizationId { get; set; }

        public CustomerUserViewModel Customer { get; set; }

        public BasicOrganizationViewModel Organization { get; set; }
    }
}
