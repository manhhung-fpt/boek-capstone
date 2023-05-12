using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class CampaignStaffViewModel
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

        public BasicCampaignViewModel Campaign { get; set; }
        [Child]
        public UserViewModel Staff { get; set; }
    }
}
