using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Schedules;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Organizations;

namespace Boek.Infrastructure.ViewModels.CampaignOrganizations
{
    public class CampaignOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? OrganizationId { get; set; }
        [Int]
        public int? CampaignId { get; set; }

        public BasicCampaignViewModel Campaign { get; set; }

        public BasicOrganizationViewModel Organization { get; set; }

        public List<SchedulesViewModel> Schedules { get; set; }
    }
}
