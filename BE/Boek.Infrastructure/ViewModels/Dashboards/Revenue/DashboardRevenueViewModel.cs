namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue
{
    public class DashboardRevenueViewModel<U, T>
    {
        public int Total { get; set; } = 0;
        public List<RevenuesViewModel<U, T>> Models { get; set; }
        public void UpdateTotal(bool? IsDescendingData = null, bool? IsDescendingTimeLine = null)
        {
            if (this.Models.Any())
            {
                Total = Models.Count();
                Models.ForEach(m => m.Model.UpdateTotal());
                var result = !IsDescendingTimeLine.HasValue || false.Equals(IsDescendingTimeLine);
                if (IsDescendingData.HasValue && result)
                {
                    if ((bool)IsDescendingData)
                        Models = Models.OrderByDescending(item => item.Model.Data.First().Total).ToList();
                    else
                        Models = Models.OrderBy(item => item.Model.Data.First().Total).ToList();
                }
            }
        }
    }
}