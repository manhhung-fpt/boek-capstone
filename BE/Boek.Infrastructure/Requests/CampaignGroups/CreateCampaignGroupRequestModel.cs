using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignGroups
{
    public class CreateCampaignGroupRequestModel
    {
        public int? CampaignId { get; set; }

        public int? GroupId { get; set; }
    }
}
