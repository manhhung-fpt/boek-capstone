using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Campaigns
{
    public class UpdateCampaignRequestModel
    {
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_ID}")]
        public int Id { get; set; }
        [MaxLength(255)]
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_NAME}")]
        public string Name { get; set; }
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.CAMPAIGN_DESCRIPTION}")]
        public string Description { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.CAMPAIGN_IMAGE}")]
        public string ImageUrl { get; set; }
    }
}
