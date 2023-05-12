using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(BoekCapstoneContext context) : base(context)
        {
        }
        public List<CampaignOrganization> GetOrganizations(int? CampaignId)
        {
            var result = _context.CampaignOrganizations.Where(oc => oc.CampaignId.Equals(CampaignId))
               .Include(oc => oc.Organization).ToList();
            return result ?? null;
        }
    }
}
