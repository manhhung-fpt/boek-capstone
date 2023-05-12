using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;

namespace Boek.Repository.Repositories
{
    public class CampaignCommissionRepository : GenericRepository<CampaignCommission>, ICampaignCommissionRepository
    {
        public CampaignCommissionRepository(BoekCapstoneContext context) : base(context)
        {
        }
    }
}
