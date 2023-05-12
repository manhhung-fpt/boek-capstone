using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface ICampaignLevelRepository : IGenericRepository<CampaignLevel>
    {
        List<Level> GetLevels(int? CampaignId);
    }
}