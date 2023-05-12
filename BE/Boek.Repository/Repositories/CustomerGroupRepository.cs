using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class CustomerGroupRepository : GenericRepository<CustomerGroup>, ICustomerGroupRepository
    {
        public CustomerGroupRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Customer> GetCustomers(int? GroupId)
        {
            var customers = new List<Customer>();
            var list =
                dbSet
                    .Where(co => co.GroupId.Equals(GroupId))
                    .Include(co => co.Customer)
                    .ThenInclude(c => c.IdNavigation);
            if (list != null)
                customers = list.Select(co => co.Customer).ToList();
            return customers;
        }

        public List<Group> GetGroups(Guid? CustomerId)
        {
            var groups = new List<Group>();
            var list =
                dbSet
                    .Where(co => co.CustomerId.Equals(CustomerId))
                    .Include(co => co.Group);
            if (list != null)
                groups = list.Select(co => co.Group).ToList();
            return groups;
        }
    }
}
