using Boek.Infrastructure.Requests.Schedules;

namespace Boek.Infrastructure.Requests.CampaignOrganizations
{
    public class CreateCampaignOrganizationRequestModel
    {
        public int? OrganizationId { get; set; }
        public int? CampaignId { get; set; }
        public List<CreateSchedulesRequestModel> Schedules { get; set; }
    }
}
