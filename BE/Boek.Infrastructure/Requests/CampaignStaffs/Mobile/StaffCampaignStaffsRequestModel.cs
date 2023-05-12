using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.CampaignStaffs.Mobile
{
    public class StaffCampaignStaffsRequestModel
    {
        [Byte]
        public byte? Status { get; set; }
    }
}