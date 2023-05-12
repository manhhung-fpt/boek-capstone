namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue
{
    public class SubRevenueListViewModel<T>
    {
        public TimeLineViewModel timeLine { get; set; }
        public int Total { get; set; } = 0;
        public List<RevenueViewModel<T>> Data { get; set; }
        public void UpdateTotal()
        {
            if (this.Data.Any())
                Total = Data.Count();
        }

        public void UpdateData(int? Size = null, bool? IsDescending = null)
        {
            if (Size.HasValue && Size > 0)
                Data = Data.Take((int)Size).ToList();
            if ((bool)IsDescending)
                Data = Data.OrderByDescending(item => item.Total).ToList();
            else
                Data = Data.OrderBy(item => item.Total).ToList();
        }
    }
}