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
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/issuer/dashboard")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerDashboardsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IDashboardService _dashboardService;
        public IssuerDashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        #endregion

        #region Gets

        #region Comparison
        /// <summary>
        /// Get orders by issuer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("orders")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_COMPARISON })]
        public ActionResult<DashboardViewModel<OrderViewModel>> GetOrdersByIssuer([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetOrdersByIssuer(request);
        }
        /// <summary>
        /// Get new book products by issuer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("books/products")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_COMPARISON })]
        public ActionResult<DashboardViewModel<BookProductViewModel>> GetBookProducts([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetBookProducts(request);
        }
        #endregion

        #region Date range
        /// <summary>
        /// Get best seller book products by issuer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("best-seller")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardViewModel<BookProductOrderDetailsViewModel>> GetBestSellerBookProductsByIssuer([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetBestSellerBookProductsByIssuer(request);
        }

        /// <summary>
        /// Get spending customers by issuer 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("spending-customers")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_DATE_RANGE })]
        public ActionResult<DashboardViewModel<CustomerOrdersViewModel>> GetTopSpendingCustomers([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetTopSpendingCustomers(request);
        }
        /// <summary>
        /// XX Campaign's revenues (XX is size data)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/revenues")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_DATE_RANGE })]
        public ActionResult<RevenueListViewModel<BasicCampaignViewModel>> GetCampaignsRevenue([FromBody] DashboardRequestModel request)
        {
            return _dashboardService.GetCampaignsRevenue(request);
        }

        /// <summary>
        /// The details of campaign's revenue by issuer
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("campaigns/revenues/details/{campaignId}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_DATE_RANGE })]
        public ActionResult<IssuerRevenuesViewModel<BasicCampaignViewModel>> GetIssuersRevenueFromCampaign(int campaignId, [FromBody] IssuerCampaignDashboardRequestModel request)
        {
            if (request != null)
                request.CampaignId = campaignId;
            return _dashboardService.GetIssuersRevenueFromCampaign(request);
        }
        #endregion

        #region Summary
        /// <summary>
        /// Get summary by issuer 
        /// </summary>
        /// <returns></returns>
        [HttpPost("summary")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_DASHBOARD_SUMMARY })]
        public ActionResult<List<SummaryViewModel>> GetSummaryDashboard()
        {
            return _dashboardService.GetSummaryDashboard();
        }
        #endregion

        #endregion
    }
}