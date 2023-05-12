using Boek.Infrastructure.Attributes;
namespace Boek.Infrastructure.Requests.CampaignGroups.Mobile
{
    public class CampaignGroupMobileRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> GroupIds { get; set; }
    }
}