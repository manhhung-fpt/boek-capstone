using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICustomerGroupRepository : IGenericRepository<CustomerGroup>
    {
        List<Customer> GetCustomers(int? GroupId);
        List<Group> GetGroups(Guid? CustomerId);
    }
}
