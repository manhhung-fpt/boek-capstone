using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class OrderCampaignStaffViewModel
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
        public UserViewModel Staff { get; set; }

    }
}