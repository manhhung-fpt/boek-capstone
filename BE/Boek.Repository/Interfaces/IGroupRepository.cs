using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Group CheckDuplicatedGroupName(string name);
    }
}
