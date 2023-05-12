using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Participants {
    public class ApplyParticipantRequestModel
    {
        [Required]
        public int? CampaignId { get; set; }
    }
}