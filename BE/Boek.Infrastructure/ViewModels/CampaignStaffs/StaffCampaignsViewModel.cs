using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;
using System.Text.Json.Serialization;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class StaffCampaignsViewModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Guid]
        public Guid? StaffId { get; set; }

        [Byte]
        public byte? Status { get; set; }

        [String]
        public string StatusName { get; set; }

        [Sort, JsonIgnore]
        public string Sort { get; set; }

        public List<BasicCampaignViewModel> Campaigns { get; set; }

        public UserViewModel Staff { get; set; }
    }
}
