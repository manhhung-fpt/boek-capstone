using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CustomerOrganizations
{
    public class BasicCustomerOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        [Int]
        public int? OrganizationId { get; set; }
    }
}
