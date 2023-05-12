using System.Text.Json.Serialization;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.Schedules;
using Boek.Infrastructure.ViewModels.Users.Issuers;

namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile
{
    public class StaffCampaignMobilesViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? Code { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [String]
        public string ImageUrl { get; set; }

        [Byte]
        public byte? Format { get; set; }

        [Byte]
        public string Address { get; set; }

        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Boolean]
        public bool? IsRecurring { get; set; }
        [Byte]
        public byte? Status { get; set; }

        [DateRange]
        public DateTime? CreatedDate { get; set; }

        [DateRange]
        public DateTime? UpdatedDate { get; set; }

        [String]
        public string StatusName { get; set; }

        [String]
        public string FormatName { get; set; }

        [Sort, JsonIgnore]
        public string Sort { get; set; }
        public List<IssuerViewModel> Issuers { get; set; }
        [JsonIgnore]
        public List<MobileCampaignStaffsViewModel> CampaignStaffs { get; set; }
        public List<SchedulesViewModel> Schedules { get; set; }
    }
}