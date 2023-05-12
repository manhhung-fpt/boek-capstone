using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Addresses;

namespace Boek.Infrastructure.ViewModels.Schedules
{
    public class SchedulesViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignOrganizationId { get; set; }
        [String]
        public string Address { get; set; }
        [Child]
        public AddressViewModel AddressViewModel { get; set; }
        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [String]
        public string StatusName { get; set; }
    }
}