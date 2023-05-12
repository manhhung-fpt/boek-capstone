using Boek.Core.Entities;

namespace Boek.Repository.Interfaces
{
    public interface IParticipantRepository : IGenericRepository<Participant>
    {
        List<Issuer> GetIssuers(int? CampaignId);
    }
}
