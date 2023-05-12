using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class CampaignGroupRepository : GenericRepository<CampaignGroup>, ICampaignGroupRepository
    {
        public CampaignGroupRepository(BoekCapstoneContext context) :
            base(context)
        {
        }

        public List<Campaign> GetCampaigns(int? GroupId)
        {
            var campaigns = new List<Campaign>();
            var list =
                dbSet
                    .Where(co => co.GroupId.Equals(GroupId))
                    .Include(co => co.Campaign);
            if (list != null)
                campaigns = list.Select(co => co.Campaign).ToList();
            return campaigns;
        }
    }
}
