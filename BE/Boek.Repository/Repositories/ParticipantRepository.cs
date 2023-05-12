using Boek.Core.Data;
using Boek.Core.Entities;
using Boek.Repository.Interfaces;
using Boek.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Boek.Repository.Repositories
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(BoekCapstoneContext context) : base(context)
        {
        }

        public List<Issuer> GetIssuers(int? CampaignId)
        {
            var issuers = new List<Issuer>();
            var list =
                dbSet.Where(p => p.CampaignId.Equals(CampaignId) &&
                (p.Status.Equals((byte)ParticipantStatus.Approved) ||
                p.Status.Equals((byte)ParticipantStatus.Accepted)))
                    .Include(p => p.Issuer)
                    .ThenInclude(p => p.IdNavigation);
            if (list != null)
                issuers = list.Select(i => i.Issuer).ToList();
            return issuers;
        }
    }
}
