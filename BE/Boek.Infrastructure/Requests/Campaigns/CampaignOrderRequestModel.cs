using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class CampaignOrderRequestModel
    {
        [String]
        public string Name { get; set; }
    }
}