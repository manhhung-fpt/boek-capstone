using Boek.Repository.Interfaces;
using Boek.Core.Entities;
using Boek.Core.Data;

namespace Boek.Repository.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public Group CheckDuplicatedGroupName(string name) =>
            dbSet.SingleOrDefault(u =>
                    u.Name.ToLower().Trim().Equals(name.ToLower().Trim()));
    }
}
