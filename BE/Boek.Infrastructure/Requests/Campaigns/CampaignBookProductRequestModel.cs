using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class CampaignBookProductRequestModel
    {
        [Range, ByteRange, RangeField]
        public List<byte?> Status { get; set; }
    }
}