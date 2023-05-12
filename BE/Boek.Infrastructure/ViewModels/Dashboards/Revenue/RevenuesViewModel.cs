namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue
{
    public class RevenuesViewModel<U, T>
    {
        public U Subject { get; set; }
        public TimeLineViewModel timeLine { get; set; }
        public SubDashboardRevenueViewModel<T> Model { get; set; }
    }
}