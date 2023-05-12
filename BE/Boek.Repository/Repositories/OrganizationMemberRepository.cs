using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class OrganizationMemberRepository : GenericRepository<OrganizationMember>, IOrganizationMemberRepository
    {
        public OrganizationMemberRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<int?> GetOrganizationIdsByEmailDomain(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            if (!email.Contains("@") || (email.IndexOf("@") + 1).Equals(email.Length))
                return null;
            var domain = email.Substring(email.IndexOf("@") + 1);
            var list = dbSet.Where(om => om.EmailDomain.Contains(domain))
            .Select(om => om.OrganizationId)
            .Distinct()
            .ToList();
            return list;
        }

        public List<OrganizationMember> GetOrganizationMembersByOrganizationId(int? OrganizationId)
        {
            if (!OrganizationId.HasValue)
                return null;
            var list = dbSet.Where(om => om.OrganizationId.Equals(OrganizationId)).ToList();
            return list;
        }
        public bool CheckDuplicatedOrganizationMemberDomain(int? OrganizationId, string EmailDomain)
        => dbSet.Any(om =>
        om.OrganizationId.Equals(OrganizationId) &&
        om.EmailDomain.ToLower().Trim().Equals(EmailDomain.ToLower().Trim()));
    }
}