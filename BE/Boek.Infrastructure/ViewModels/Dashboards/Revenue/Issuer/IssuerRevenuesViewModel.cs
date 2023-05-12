using Boek.Infrastructure.ViewModels.BookProducts;
using Boek.Infrastructure.ViewModels.Orders;

namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer
{
    public class IssuerRevenuesViewModel<T>
    {
        public TimeLineViewModel timeLine { get; set; }
        public T Subject { get; set; }
        public int RevenueTotal { get; set; } = 0;
        public List<IssuerRevenueViewModel> Revenues { get; set; }
        public IssuerSubDashboardViewModel<BasicBookProductViewModel> BestSellerBookProducts { get; set; }
        public DashboardViewModel<OrdersViewModel> Orders { get; set; }
        public void UpdateTotal(bool? IsDescendingData = null, bool? IsDescendingTimeLine = null)
        {
            if (this.Revenues != null)
            {
                if (this.Revenues.Any())
                    RevenueTotal = Revenues.Count();
            }
            if (this.BestSellerBookProducts != null)
            {
                if (this.BestSellerBookProducts.Data.Any())
                    this.BestSellerBookProducts.UpdateTotal(IsDescendingData);
            }
            if (this.Orders != null)
                this.Orders.UpdateTotal(IsDescendingData, IsDescendingTimeLine);
        }
    }
}