using Boek.Infrastructure.Requests.Schedules;

namespace Boek.Infrastructure.Requests.CampaignOrganizations
{
    public class CreateCampaignOrganizationsRequestModel
    {
        public int? OrganizationId { get; set; }
        public List<CreateSchedulesRequestModel> Schedules { get; set; }
    }
}
