using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Participants
{
    public class ParticipantRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Guid]
        public Guid? IssuerId { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
        [String]
        public string Note { get; set; }
        [Range, ByteRange, RangeField]
        public List<byte?> Status { get; set; }
        public bool NotPaging { get; set; } = false;

    }
}