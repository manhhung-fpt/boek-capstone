using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Participants {
    public class UpdateParticipantRequestModel
    {
        [Required]
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public Guid? IssuerId { get; set; }
        public byte Status { get; set; }
        public string Note { get; set; }
    }
}