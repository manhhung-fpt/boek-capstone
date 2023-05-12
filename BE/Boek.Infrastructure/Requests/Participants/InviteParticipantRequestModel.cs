namespace Boek.Infrastructure.Requests.Participants{
    public class InviteParticipantRequestModel 
    {
        public int? CampaignId { get; set; }
        public List<Guid?> Issuers { get; set; }
    }
}