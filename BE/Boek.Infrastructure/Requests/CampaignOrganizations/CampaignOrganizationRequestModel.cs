using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignOrganizations
{
    public class CampaignOrganizationRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? OrganizationId { get; set; }
        [Int]
        public int? CampaignId { get; set; }
    }
}
