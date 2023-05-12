using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignLevels.Mobile
{
    public class CampaignLevelMobileRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> LevelIds { get; set; }
    }
}