using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Users;

namespace Boek.Infrastructure.Requests.CampaignStaffs
{
    public class CampaignStaffRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [Int]
        public int? CampaignId { get; set; }

        [Guid]
        public Guid? StaffId { get; set; }

        [Byte]
        public byte? Status { get; set; }

        [Sort]
        public string Sort { get; set; }
        [Child]
        public UserRequestModel Staff { get; set; }
    }
}
