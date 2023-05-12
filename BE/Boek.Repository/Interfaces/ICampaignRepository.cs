using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        public List<CampaignOrganization> GetOrganizations(int? CampaignId);
    }
}
