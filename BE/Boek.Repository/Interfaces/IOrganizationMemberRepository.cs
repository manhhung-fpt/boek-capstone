using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IOrganizationMemberRepository : IGenericRepository<OrganizationMember>
    {
        List<int?> GetOrganizationIdsByEmailDomain(string email);
        List<OrganizationMember> GetOrganizationMembersByOrganizationId(int? OrganizationId);
        bool CheckDuplicatedOrganizationMemberDomain(int? OrganizationId, string EmailDomain);
    }
}