using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class CampaignLevelRepository : GenericRepository<CampaignLevel>, ICampaignLevelRepository
    {
        public CampaignLevelRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Level> GetLevels(int? CampaignId)
        {
            var levels = new List<Level>();
            var list =
                dbSet
                    .Where(co => co.CampaignId.Equals(CampaignId))
                    .Include(co => co.Level);
            if (list != null)
                levels = list.Select(co => co.Level).ToList();
            return levels;
        }
    }
}