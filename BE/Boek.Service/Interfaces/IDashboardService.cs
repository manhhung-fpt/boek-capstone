using Boek.Infrastructure.Requests.Dashboards;
using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Dashboards;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue;
using Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer;
using Boek.Infrastructure.ViewModels.Orders;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;

namespace Boek.Service.Interfaces
{
    public interface IDashboardService
    {
        #region System
        public DashboardViewModel<BasicCampaignViewModel> GetCreatedCampaigns(DashboardRequestModel request);
        public DashboardViewModel<CustomerViewModel> GetNewCustomers(DashboardRequestModel request);
        /// <summary>
        /// Doanh thu của N (x) hội sách (Issuer, Admin)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RevenueListViewModel<BasicCampaignViewModel> GetCampaignsRevenue(DashboardRequestModel request);
        public List<SummaryViewModel> GetSummaryDashboard();
        #endregion

        #region Stakeholders
        public DashboardViewModel<CampaignParticipantsViewModel> GetParticipantsFromCampaign(DashboardRequestModel request);
        public DashboardViewModel<OrderViewModel> GetOrdersByAdmin(DashboardRequestModel request);
        public DashboardViewModel<OrderViewModel> GetOrdersByIssuer(DashboardRequestModel request);
        public DashboardViewModel<BookProductViewModel> GetBookProducts(DashboardRequestModel request);
        /// <summary>
        /// Top N (x) doanh thu từ NPH của N (y) hội sách
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel> GetIssuersRevenueFromCampaignsByAdmin(DashboardRequestModel request);
        /// <summary>
        /// Doanh thu giữa N(x) NPH trong cùng một hội sách
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DashboardRevenueViewModel<BasicCampaignViewModel, IssuerViewModel> GetIssuersRevenueFromCampaign(CampaignDashboardRequestModel request);
        /// <summary>
        /// Doanh thu, sách bán, và đơn hàng của một hội sách (theo khoảng thời gian)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IssuerRevenuesViewModel<BasicCampaignViewModel> GetIssuersRevenueFromCampaign(IssuerCampaignDashboardRequestModel request);
        /// <summary>
        /// Top N (x) doanh thu của (y) NPH
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DashboardRevenueViewModel<IssuerViewModel, BasicCampaignViewModel> GetIssuersRevenueByAdmin(DashboardRequestModel request);
        #endregion

        #region Customers
        public DashboardViewModel<CustomerOrdersViewModel> GetTopSpendingCustomers(DashboardRequestModel request);
        public DashboardViewModel<BookProductOrderDetailsViewModel> GetBestSellerBookProductsByAdmin(DashboardRequestModel request);
        public DashboardViewModel<BookProductOrderDetailsViewModel> GetBestSellerBookProductsByIssuer(DashboardRequestModel request);
        #endregion
    }
}