using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.CampaignOrganizations.Mobile;
using Boek.Infrastructure.Requests.Users.Issuers.Mobile;

namespace Boek.Infrastructure.Requests.Campaigns.Mobile
{
    public class StaffCampaignMobileFilterRequestModel
    {
        [String]
        public string Name { get; set; }
        [String]
        public string Address { get; set; }
        [Range, ByteRange]
        public List<byte?> Formats { get; set; }
        [Child]
        public StaffIssuerMobileRequestModel Issuers { get; set; }
        [Child]
        public CampaignOrganizationMobileFilterRequestModel CampaignOrganizations { get; set; }
    }
}