using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BoekCapstoneContext context) : base(context)
        {
        }
    }
}
