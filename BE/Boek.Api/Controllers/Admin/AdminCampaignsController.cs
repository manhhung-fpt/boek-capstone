using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/campaigns")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminCampaignsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly ICampaignService _campaignService;
        private readonly ICampaignCommissionService _campaignCommissionService;
        private readonly ICampaignStaffService _campaignStaffService;

        private readonly ICampaignOrganizationService _campaignOrganizationService;
        /*private readonly ICampaignBookService _campaignBookService;*/
        public AdminCampaignsController(ICampaignService campaignService,
            ICampaignCommissionService campaignCommissionService,
        ICampaignStaffService campaignStaffService,
            ICampaignOrganizationService campaignOrganizationService)
        {
            _campaignService = campaignService;
            _campaignCommissionService = campaignCommissionService;
            _campaignStaffService = campaignStaffService;
            _campaignOrganizationService = campaignOrganizationService;
        }
        #endregion

        #region Campaigns
        #region Gets
        /// <summary>
        /// Get all campaigns by admin
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<BaseResponsePagingModel<CampaignViewModel>> GetCampaigns([FromQuery] CampaignRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignService.GetCampaignsByAdmin(filter, paging);
        }

        /// <summary>
        /// Get a campaign by id by admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithAddressDetail"></param>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<CampaignViewModel> GetCampaignByIdByAdmin(int id, bool WithAddressDetail = false)
        {
            return _campaignService.GetCampaignByIdByAdmin(id, WithAddressDetail);
        }
        #endregion

        #region Offline
        /// <summary>
        /// Create an offline campaign by admin
        /// </summary>
        /// <param name="createOfflineCampaign"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPost("offline")]
        public ActionResult<CampaignViewModel> CreateOfflineCampaign([FromBody] CreateOfflineCampaignRequestModel createOfflineCampaign)
        {
            return _campaignService.CreateOfflineCampaign(createOfflineCampaign);
        }

        /// <summary>
        /// Update an offline campaign by admin
        /// </summary>
        /// <param name="updateOfflineCampaign"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPut("offline")]
        public ActionResult<CampaignViewModel> UpdateOfflineCampaign([FromBody] UpdateOfflineCampaignRequestModel updateOfflineCampaign)
        {
            return _campaignService.UpdateOfflineCampaign(updateOfflineCampaign);
        }

        /// <summary>
        /// Cancel an offline campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("offline/cancellation/{id}")]
        public ActionResult<CampaignViewModel> CancelOfflineCampaign(int id)
        {
            return _campaignService.CancelOfflineCampaign(id);
        }
        /// <summary>
        /// Postpone an offline campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("offline/postponement/{id}")]
        public ActionResult<CampaignViewModel> PostponeOfflineCampaign(int id)
        {
            return _campaignService.PostponeOfflineCampaign(id);
        }
        /// <summary>
        /// Restart an offline campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("offline/restart/{id}")]
        public ActionResult<CampaignViewModel> RestartOfflineCampaign(int id)
        {
            return _campaignService.RestartOfflineCampaign(id);
        }

        #endregion

        #region Online
        /// <summary>
        /// Create an online campaign by admin
        /// </summary>
        /// <param name="createOnlineCampaign"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPost("online")]
        public ActionResult<CampaignViewModel> CreateOnlineCampaign([FromBody] CreateOnlineCampaignRequestModel createOnlineCampaign)
        {
            return _campaignService.CreateOnlineCampaign(createOnlineCampaign);
        }

        /// <summary>
        /// Update an online campaign by admin
        /// </summary>
        /// <param name="updateOnlineCampaign"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPut("online")]
        public ActionResult<CampaignViewModel> UpdateOnlineCampaign([FromBody] UpdateOnlineCampaignRequestModel updateOnlineCampaign)
        {
            return _campaignService.UpdateOnlineCampaign(updateOnlineCampaign);
        }

        /// <summary>
        /// Cancel an online campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("online/cancellation/{id}")]
        public ActionResult<CampaignViewModel> CancelOnlineCampaign(int id)
        {
            return _campaignService.CancelOnlineCampaign(id);
        }

        /// <summary>
        /// Postpone an offline campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("online/postponement/{id}")]
        public ActionResult<CampaignViewModel> PostponeOnlineCampaign(int id)
        {
            return _campaignService.PostponeOnlineCampaign(id);
        }
        /// <summary>
        /// Restart an offline campaign by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("online/restart/{id}")]
        public ActionResult<CampaignViewModel> RestartOnlineCampaign(int id)
        {
            return _campaignService.RestartOnlineCampaign(id);
        }
        #endregion

        #region Update Started Campaign
        /// <summary>
        /// Update the basic info of a started campaign
        /// </summary>
        /// <param name="updateCampaign"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPut("started")]
        public ActionResult<CampaignViewModel> UpdateStartedCampaign([FromBody] UpdateCampaignRequestModel updateCampaign)
        {
            return _campaignService.UpdateStartedCampaign(updateCampaign);
        }

        #endregion

        #endregion

        #region Get Unparticipated Users
        /// <summary>
        /// Get unparticipated issuers by campaign id by admin
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("unparticipated-issuers/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<IEnumerable<UserViewModel>> GetUnparticipatedIssuers(int id)
        {
            return _campaignService.GetUnparticipatedIssuers(id).ToList();
        }
        #endregion

        #region Campaign Staff
        #region Gets
        /// <summary>
        /// Get all campaign staff
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("staff")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<BaseResponsePagingModel<CampaignStaffViewModel>> GetCampaignStaffs([FromQuery] CampaignStaffRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignStaffService.GetCampaignStaffs(filter, paging);
        }
        /// <summary>
        /// Get a campaign's staff by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("staff/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<CampaignStaffViewModel> GetCampaignStaffById(int id)
        {
            return _campaignStaffService.GetCampaignStaffById(id);
        }
        /// <summary>
        /// Get a campaign's staff by campaign id
        /// </summary>
        /// <param name="campaignid"></param>
        [HttpGet("{campaignid}/staff")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public ActionResult<CampaignStaffsViewModel> GetCampaignStaffsByCampaignId(int campaignid)
        {
            return _campaignStaffService.GetCampaignStaffsByCampaignId(campaignid);
        }
        /// <summary>
        /// Get a campaign's unattended staff by campaign id
        /// </summary>
        /// <param name="campaignid"></param>
        [HttpGet("{campaignid}/unattended-staff")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        public IEnumerable<UserViewModel> GetUnattendedCampaignStaffs(int campaignid)
        {
            return _campaignStaffService.GetUnattendedCampaignStaffs(campaignid);
        }
        #endregion

        #region Updates
        /// <summary>
        /// Update an attended status of campaign staff by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("staff/attended-staff/{id}")]
        public ActionResult<BasicCampaignStaffViewModel> UpdateCampaignStaffStatusIntoAttendedStatus(int id)
        {
            return _campaignStaffService.UpdateCampaignStaffStatusIntoAttendedStatus(id);
        }
        /// <summary>
        /// Update an unattended status of campaign staff by admin
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPatch("staff/unattended-staff/{id}")]
        public ActionResult<BasicCampaignStaffViewModel> UpdateCampaignStaffStatusIntoUnAttendedStatus(int id)
        {
            return _campaignStaffService.UpdateCampaignStaffStatusIntoUnAttendedStatus(id);
        }
        #endregion

        #region Create
        /// <summary>
        /// Create campaign staff by admin
        /// </summary>
        /// <param name="createdCampaignStaff"></param>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_CAMPAIGN })]
        [HttpPost("staff")]
        public ActionResult<List<BasicCampaignStaffViewModel>> CreateCampaignStaffs([FromBody] CreateCampaignStaffRequestModel createdCampaignStaff)
        {
            return _campaignStaffService.CreateCampaignStaffs(createdCampaignStaff);
        }
        #endregion
        #endregion
    }
}
