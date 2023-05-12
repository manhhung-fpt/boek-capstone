using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICustomerOrganizationRepository : IGenericRepository<CustomerOrganization>
    {
        List<Customer> GetCustomers(int? OrganizationId);

        List<Organization> GetOrganizations(Guid? CustomerId);
    }
}
