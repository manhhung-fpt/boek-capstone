using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class OrganizationRepository : GenericRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(BoekCapstoneContext context) :
            base(context)
        {
        }

        public Organization CheckDuplicatedOrganizationName(string name) =>
            dbSet.SingleOrDefault(u =>
                    u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }
}
