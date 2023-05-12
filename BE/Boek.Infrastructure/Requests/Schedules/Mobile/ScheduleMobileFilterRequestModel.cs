using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Schedules.Mobile
{
    public class ScheduleMobileFilterRequestModel
    {
        [Range, StringRange, RangeField]
        public List<string> Address { get; set; }
    }
}