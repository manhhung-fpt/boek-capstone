using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Infrastructure.Requests.Dashboards;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Dashboards;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/dashboard")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminDashboardController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IDashboardService _dashboardService;
        public AdminDashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        #endregion

        #region Gets

        #region Comparison
        /// <summary>
        /// Get created campaigns by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("created-campaigns")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_COMPARISON })]
        public ActionResult<DashboardViewModel<BasicCampaignViewModel>> GetCreatedCampaigns([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetCreatedCampaigns(request);
        }

        /// <summary>
        /// Get new customers by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("new-customers")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_COMPARISON })]
        public ActionResult<DashboardViewModel<CustomerViewModel>> GetNewCustomers([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetNewCustomers(request);
        }

        /// <summary>
        /// Get orders by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("orders")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_COMPARISON })]
        public ActionResult<DashboardViewModel<OrderViewModel>> GetOrdersByAdmin([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetOrdersByAdmin(request);
        }
        #endregion

        #region Date range
        /// <summary>
        /// Get participants by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("participants")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardViewModel<CampaignParticipantsViewModel>> GetParticipantsFromCampaign([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetParticipantsFromCampaign(request);
        }
        /// <summary>
        /// Get spending customers by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("spending-customers")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardViewModel<CustomerOrdersViewModel>> GetTopSpendingCustomers([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetTopSpendingCustomers(request);
        }

        /// <summary>
        /// Get best seller book products by admin 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("best-seller")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardViewModel<BookProductOrderDetailsViewModel>> GetBestSellerBookProductsByAdmin([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetBestSellerBookProductsByAdmin(request);
        }

        /// <summary>
        /// XX Campaign's revenues (XX is size data)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/revenues")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<RevenueListViewModel<BasicCampaignViewModel>> GetCampaignsRevenue([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetCampaignsRevenue(request);
        }

        /// <summary>
        /// Top XX issuer's revenue in YY campaign (XX is size data, and YY is size subject)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/issuer/revenues/top")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel>> GetIssuersRevenueFromCampaignsByAdmin([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetIssuersRevenueFromCampaignsByAdmin(request);
        }
        /// <summary>
        /// XX issuer's revenues in a campaign (XX is size data)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/issuer/revenues")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel>> GetIssuersRevenueFromCampaign([FromBody] CampaignDashboardRequestModel request)
        {
            return _dashboardService.GetIssuersRevenueFromCampaign(request);
        }
        /// <summary>
        /// The details of campaign's revenue
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/revenues/details/{campaignId}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<IssuerRevenuesViewModel<BasicCampaignViewModel>> GetIssuersRevenueFromCampaign(int campaignId, [FromBody] IssuerCampaignDashboardRequestModel request)
        {
            if (request != null)
                request.CampaignId = campaignId;
            return _dashboardService.GetIssuersRevenueFromCampaign(request);
        }
        /// <summary>
        /// Top XX revenues of YY issuers (XX is size data, and YY is size subject)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("issuer/campaigns/revenues")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardRevenueViewModel<IssuerViewModel, BasicCampaignViewModel>> GetIssuersRevenueByAdmin([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetIssuersRevenueByAdmin(request);
        }
        #endregion

        #region Summary
        /// <summary>
        /// Get summary by admin 
        /// </summary>
        /// <returns></returns>
        [HttpPost("summary")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_DASHBOARD_SUMMARY })]
        public ActionResult<List<SummaryViewModel>> GetSummaryDashboard()
        {
            return _dashboardService.GetSummaryDashboard();
        }
        #endregion
        #endregion
    }
}