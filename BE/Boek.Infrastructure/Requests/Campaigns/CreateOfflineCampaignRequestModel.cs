using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Boek.Infrastructure.Requests.Addresses;
using Boek.Infrastructure.Requests.CampaignCommissions;
using Boek.Infrastructure.Requests.CampaignOrganizations;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class CreateOfflineCampaignRequestModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Url]
        public string ImageUrl { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<CampaignCommissionsRequestModel> CampaignCommissions { get; set; }
        public List<CreateCampaignOrganizationsRequestModel> CampaignOrganizations { get; set; }
    }
}
