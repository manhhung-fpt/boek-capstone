using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class CampaignRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? Code { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [String]
        public string ImageUrl { get; set; }

        [Byte]
        public byte? Format { get; set; }

        [String]
        public string Address { get; set; }
        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Boolean]
        public bool? IsRecurring { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
        [Sort]
        public string Sort { get; set; }
        [Skip]
        public bool WithAddressDetail { get; set; } = false;
    }
}
