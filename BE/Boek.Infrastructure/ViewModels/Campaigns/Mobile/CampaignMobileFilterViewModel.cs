
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.CampaignGroups.Mobile;
using Boek.Infrastructure.Requests.CampaignLevels.Mobile;
using Boek.Infrastructure.Requests.CampaignOrganizations.Mobile;
using Boek.Infrastructure.Requests.Participants.Mobile;

namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile
{
    public class CampaignMobileFilterViewModel
    {
        [String]
        public string Name { get; set; }
        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Boolean]
        public bool? IsRecurring { get; set; }
        [Range, StringRange, RangeField]
        public List<string> Address { get; set; }
        [Range, ByteRange]
        public List<byte?> Formats { get; set; }
        [Range, ByteRange, RangeField]
        public List<byte?> Status { get; set; }
        [Sort]
        public string Sort { get; set; }
        [Child]
        public ParticipantMobileFilterRequestModel Participants { get; set; }
        [Child]
        public CampaignLevelMobileRequestModel CampaignLevels { get; set; }
        [Child]
        public CampaignGroupMobileRequestModel CampaignGroups { get; set; }
        [ChildRange]
        public CampaignOrganizationMobileFilterRequestModel CampaignOrganizations { get; set; }
    }
}