using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Groups;

namespace Boek.Infrastructure.ViewModels.CampaignGroups
{
    public class CampaignGroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Int]
        public int? GroupId { get; set; }

        public BasicCampaignViewModel Campaign { get; set; }

        public BasicGroupViewModel Group { get; set; }
    }
}
