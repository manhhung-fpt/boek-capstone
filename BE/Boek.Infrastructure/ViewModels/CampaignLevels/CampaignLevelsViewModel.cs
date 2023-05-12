using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Levels;

namespace Boek.Infrastructure.ViewModels.CampaignLevels
{
    public class CampaignLevelsViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? LevelId { get; set; }
        public BasicLevelViewModel Level { get; set; }
    }
}