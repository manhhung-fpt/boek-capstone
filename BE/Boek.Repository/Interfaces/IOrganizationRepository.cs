using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IOrganizationRepository : IGenericRepository<Organization>
    {
        Organization CheckDuplicatedOrganizationName(string name);
    }
}
