using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Service.Interfaces.Mobile;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.Requests.Campaigns.Mobile;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Staff
{
    [Route("api/staff/campaigns")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Staff))]
    public class StaffCampaignsController : ControllerBase
    {
        #region Field(s) and constructor 
        private readonly ICampaignMobileService _campaignMobileService;
        public StaffCampaignsController(ICampaignMobileService campaignMobileService)
        {
            _campaignMobileService = campaignMobileService;
        }
        #endregion

        #region Campaign Mobile

        /// <summary>
        /// Get a staff's mobile campaigns
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_STAFF_CAMPAIGN })]
        public ActionResult<List<HierarchicalStaffCampaignsViewModel>> GetStaffCampaigns([FromQuery] StaffCampaignMobileRequestModel filter)
        {
            return _campaignMobileService.GetStaffCampaigns(filter);
        }
        #endregion
    }
}