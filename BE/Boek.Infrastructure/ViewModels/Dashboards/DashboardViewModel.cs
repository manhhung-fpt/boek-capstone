namespace Boek.Infrastructure.ViewModels.Dashboards
{
    public class DashboardViewModel<T>
    {
        public int Total { get; set; } = 0;
        public List<SubDashboardViewModel<T>> models { get; set; }

        public void UpdateTotal(bool? IsDescendingData = null, bool? IsDescendingTimeLine = null)
        {
            if (this.models != null)
            {
                if (this.models.Any())
                {
                    Total = models.Count();
                    models.ForEach(m => m.UpdateTotal());
                    var result = !IsDescendingTimeLine.HasValue || false.Equals(IsDescendingTimeLine);
                    if (IsDescendingData.HasValue && result)
                    {
                        if ((bool)IsDescendingData)
                            models = models.OrderByDescending(item => item.Total).ToList();
                        else
                            models = models.OrderBy(item => item.Total).ToList();
                    }
                }
            }
        }
    }
}