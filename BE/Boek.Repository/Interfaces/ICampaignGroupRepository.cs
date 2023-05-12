using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICampaignGroupRepository : IGenericRepository<CampaignGroup>
    {
        List<Campaign> GetCampaigns(int? GroupId);
    }
}
