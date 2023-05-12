using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CampaignOrganizations
{
    public class BasicCampaignOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? OrganizationId { get; set; }
        [Int]
        public int? CampaignId { get; set; }
    }
}
