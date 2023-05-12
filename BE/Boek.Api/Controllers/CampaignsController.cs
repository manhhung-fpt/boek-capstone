using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Requests.Campaigns.Mobile;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Service.Interfaces.Mobile;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        #region Field(s) and constructor 
        private readonly ICampaignService _campaignService;
        private readonly ICampaignStaffService _campaignStaffService;
        private readonly ICampaignMobileService _campaignMobileService;
        private readonly IBookProductMobileService _bookProductMobileService;

        public CampaignsController(ICampaignService campaignService,
        ICampaignStaffService campaignStaffService,
        ICampaignMobileService campaignMobileService,
        IBookProductMobileService bookProductMobileService)
        {
            this._campaignService = campaignService;
            _campaignStaffService = campaignStaffService;
            _campaignMobileService = campaignMobileService;
            _bookProductMobileService = bookProductMobileService;
        }
        #endregion

        #region Campaign
        /// <summary>
        /// Get all campaigns
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<CampaignViewModel>> GetCampaigns([FromQuery] CampaignRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignService.GetCampaigns(filter, paging);
        }

        /// <summary>
        /// Get campaign by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public ActionResult<CampaignViewModel> GetCampaignById(int id)
        {
            return _campaignService.GetCampaignById(id);
        }
        #endregion

        #region Campaign Staff
        /// <summary>
        /// Get a staff's campaigns by staff id
        /// </summary>
        [HttpGet("staff")]
        [Authorize(Roles = nameof(BoekRole.Staff))]
        public ActionResult<StaffCampaignsViewModel> GetStaffCampaignsByStaffId()
        {
            return _campaignStaffService.GetStaffCampaignsByStaffId();
        }
        #endregion

        #region Campaign Schedules
        /// <summary>
        /// Get all campaign schedules by staff
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="scheduleStatus"></param>
        /// <param name="WithOccurringProvinces"></param>
        /// <returns></returns>
        [HttpGet("schedules")]
        [Authorize(Roles = nameof(BoekRole.Staff))]
        public ActionResult<BaseResponsePagingModel<CampaignSchedulesViewModel>> GetCampaignSchedules([FromQuery] CampaignRequestModel filter,
            [FromQuery] PagingModel paging, [FromQuery] List<byte?> scheduleStatus, [FromQuery] bool WithOccurringProvinces = false)
        {
            return _campaignService.GetCampaignSchedules(filter, paging, scheduleStatus, WithOccurringProvinces);
        }

        /// <summary>
        /// Get a campaign's schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheduleStatus"></param>
        /// <param name="WithOccurringProvinces"></param>
        [HttpGet("schedules/{id}")]
        public ActionResult<CampaignSchedulesViewModel> GetCampaignSchedule(int id, [FromQuery] List<byte?> scheduleStatus, [FromQuery] bool WithOccurringProvinces = false)
        {
            return _campaignService.GetCampaignSchedule(id, scheduleStatus, WithOccurringProvinces);
        }
        #endregion

        #region Customer Campaign
        /// <summary>
        /// Search customer campaigns
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet("customer")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_CAMPAIGN })]
        public ActionResult<BaseResponsePagingModel<CampaignViewModel>> SearchMobileCampaigns([FromQuery] CampaignMobileRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _campaignMobileService.GetCampaigns(filter, paging);
        }
        /// <summary>
        /// Get customer campaign by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("customer/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_CAMPAIGN })]
        public ActionResult<CampaignMobileViewModel> GetCampaignMobileById(int id)
        {
            //cSpell:disable
            var result = _campaignMobileService.GetCampaignMobileById(id);
            var hierarchicals = _bookProductMobileService.GenerateHierarchicalBookProducts(result);
            if (hierarchicals.Any())
                result.HierarchicalBookProducts = hierarchicals;
            var unhierarchicals = _bookProductMobileService.GenerateUnhierarchicalBookProducts(result);
            if (unhierarchicals.Any())
                result.UnhierarchicalBookProducts = unhierarchicals;
            return result;
        }

        /// <summary>
        /// Get customer campaigns for home page
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("customer/home-page")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_CUSTOMER_CAMPAIGN })]
        public ActionResult<CustomerCampaignMobileViewModel> GetCustomerCampaignsForHomePage(int size = 5)
        {
            return _campaignMobileService.GetCustomerCampaigns(size);
        }
        #endregion
    }
}
