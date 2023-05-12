using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Schedules
{
    public class ScheduleRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? CampaignOrganizationId { get; set; }
        [String]
        public string Address { get; set; }
        [StartDate]
        public DateTime? StartDate { get; set; }
        [EndDate]
        public DateTime? EndDate { get; set; }
        [Byte]
        public byte? Status { get; set; }
    }
}