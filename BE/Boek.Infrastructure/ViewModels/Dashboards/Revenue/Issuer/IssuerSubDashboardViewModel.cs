namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer
{
    public class IssuerSubDashboardViewModel<T>
    {
        public TimeLineViewModel timeLine { get; set; }
        public int Total { get; set; } = 0;
        public List<RevenueViewModel<T>> Data { get; set; } = new List<RevenueViewModel<T>>();
        public void UpdateTotal(bool? IsDescendingData = null)
        {
            if (this.Data.Any())
            {
                Total = Data.Count();
                if (IsDescendingData.HasValue)
                {
                    if ((bool)IsDescendingData)
                        Data = Data.OrderByDescending(item => item.Total).ToList();
                    else
                        Data = Data.OrderBy(item => item.Total).ToList();
                }
            }
        }
    }
}