using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class MobileCampaignStaffsViewModel
    {
        [Guid]
        public Guid? StaffId { get; set; }

        [Byte]
        public byte? Status { get; set; }

        [String]
        public string StatusName { get; set; }

        public UserViewModel Staff { get; set; }
    }
}