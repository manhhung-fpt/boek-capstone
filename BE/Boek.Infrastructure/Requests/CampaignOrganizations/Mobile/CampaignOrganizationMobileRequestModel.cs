using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignOrganizations.Mobile
{
    public class CampaignOrganizationMobileRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> OrganizationIds { get; set; }
    }
}