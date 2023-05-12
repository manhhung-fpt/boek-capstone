using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignLevels
{
    public class CampaignLevelRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Int]
        public int? LevelId { get; set; }
    }
}