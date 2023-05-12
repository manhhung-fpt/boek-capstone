using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.CampaignStaffs.Mobile;
using Boek.Infrastructure.Requests.Users.Issuers.Mobile;

namespace Boek.Infrastructure.Requests.Campaigns.Mobile
{
    public class StaffCampaignMobileRequestModel
    {
        [String]
        public string Name { get; set; }
        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [String]
        public string Address { get; set; }
        [Range, ByteRange]
        public List<byte?> Formats { get; set; }
        [Range, ByteRange, RangeField]
        public List<byte?> Status { get; set; }
        [Skip]
        public bool IsHomePage { get; set; } = true;
        [Child]
        public StaffIssuerMobileRequestModel Issuers { get; set; }
        [Child]
        public StaffCampaignStaffsRequestModel CampaignStaffs { get; set; }
    }
}