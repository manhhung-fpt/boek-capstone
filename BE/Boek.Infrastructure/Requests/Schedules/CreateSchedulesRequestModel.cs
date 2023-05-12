using System.Text.Json.Serialization;
using Boek.Infrastructure.Requests.Addresses;

namespace Boek.Infrastructure.Requests.Schedules
{
    public class CreateSchedulesRequestModel
    {
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}