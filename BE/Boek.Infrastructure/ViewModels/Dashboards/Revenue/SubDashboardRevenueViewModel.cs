namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue
{
    public class SubDashboardRevenueViewModel<T>
    {
        public int Total { get; set; } = 0;
        public List<RevenueViewModel<T>> Data { get; set; }
        public void UpdateTotal()
        {
            if (this.Data.Any())
                Total = Data.Count();
        }
    }
}