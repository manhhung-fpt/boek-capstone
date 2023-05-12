using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CampaignLevels
{
    public class BasicCampaignLevelViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? LevelId { get; set; }
    }
}