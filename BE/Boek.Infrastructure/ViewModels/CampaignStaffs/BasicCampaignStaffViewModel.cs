using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.CampaignStaffs
{
    public class BasicCampaignStaffViewModel
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
    }
}
