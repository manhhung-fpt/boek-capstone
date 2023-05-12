using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class
    CustomerOrganizationRepository
    : GenericRepository<CustomerOrganization>, ICustomerOrganizationRepository
    {
        public CustomerOrganizationRepository(BoekCapstoneContext context) :
            base(context)
        {
        }

        public List<Customer> GetCustomers(int? OrganizationId)
        {
            var customers = new List<Customer>();
            var list =
                dbSet
                    .Where(co => co.OrganizationId.Equals(OrganizationId))
                    .Include(co => co.Customer)
                    .ThenInclude(c => c.IdNavigation);
            if (list != null)
                customers = list.Select(co => co.Customer).ToList();
            return customers;
        }

        public List<Organization> GetOrganizations(Guid? CustomerId)
        {
            var organizations = new List<Organization>();
            var list =
                dbSet
                    .Where(co => co.CustomerId.Equals(CustomerId))
                    .Include(co => co.Organization);
            if (list != null)
                organizations = list.Select(co => co.Organization).ToList();
            return organizations;
        }
    }
}
