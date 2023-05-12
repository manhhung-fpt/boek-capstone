using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Levels;

namespace Boek.Infrastructure.ViewModels.CampaignLevels
{
    public class CampaignLevelViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? LevelId { get; set; }
        public BasicCampaignViewModel Campaign { get; set; }
        public BasicLevelViewModel Level { get; set; }
    }
}