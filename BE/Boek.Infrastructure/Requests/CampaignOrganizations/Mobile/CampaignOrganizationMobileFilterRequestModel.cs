using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Schedules.Mobile;

namespace Boek.Infrastructure.Requests.CampaignOrganizations.Mobile
{
    public class CampaignOrganizationMobileFilterRequestModel
    {
        [Range, IntRange, SkipRange]
        public List<int?> OrganizationIds { get; set; }
        [ChildRange]
        public ScheduleMobileFilterRequestModel Schedules { get; set; }
    }
}