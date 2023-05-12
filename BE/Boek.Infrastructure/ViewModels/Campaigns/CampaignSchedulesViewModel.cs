using Boek.Core.Constants;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Schedules;

namespace Boek.Infrastructure.ViewModels.Campaigns
{
    public class CampaignSchedulesViewModel
    {

        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? Code { get; set; }
        [String]
        public string Name { get; set; }
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
        [String]
        public string StatusName { get; set; }
        [String]
        public string FormatName { get; set; }

        public List<Province> OccurringProvinces { get; set; }
        public List<SchedulesViewModel> Schedules { get; set; }
    }
}