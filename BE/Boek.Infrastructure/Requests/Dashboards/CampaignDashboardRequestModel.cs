using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Dashboards
{
    public class CampaignDashboardRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_ID}")]
        public int CampaignId { get; set; }
        public int? SizeData { get; set; } = 3;
        public bool? IsDescendingData { get; set; }

        public void SetDefaultSizeData(int Number = 3)
        {
            if (!SizeData.HasValue || SizeData <= 0)
                SizeData = Number;
        }
        public void SetDescending(bool Sort = true)
        {
            if (!IsDescendingData.HasValue)
                IsDescendingData = Sort;
        }
    }
}