using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CustomerOrganizations
{
    public class CustomerOrganizationRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [Guid]
        public Guid? CustomerId { get; set; }

        [Int]
        public int? OrganizationId { get; set; }
    }
}
