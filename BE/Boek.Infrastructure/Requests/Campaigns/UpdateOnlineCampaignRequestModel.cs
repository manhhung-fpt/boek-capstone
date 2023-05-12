using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.CampaignCommissions;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class UpdateOnlineCampaignRequestModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.CAMPAIGN_IMAGE}")]
        public string ImageUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<CampaignCommissionsRequestModel> CampaignCommissions { get; set; }
        public List<int?> Groups { get; set; }
        public List<int?> Levels { get; set; }
    }
}
