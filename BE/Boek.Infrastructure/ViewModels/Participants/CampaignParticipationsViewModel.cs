using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Users.Issuers;

namespace Boek.Infrastructure.ViewModels.Participants
{
    public class CampaignParticipationsViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Guid]
        public Guid? IssuerId { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [String]
        public string StatusName { get; set; }
        [String]
        public string Note { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
        public IssuerViewModel Issuer { get; set; }
    }
}