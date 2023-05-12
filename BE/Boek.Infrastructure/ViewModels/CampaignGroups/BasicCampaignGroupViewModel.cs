using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CampaignGroups
{
    public class BasicCampaignGroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Int]
        public int? GroupId { get; set; }
    }
}
