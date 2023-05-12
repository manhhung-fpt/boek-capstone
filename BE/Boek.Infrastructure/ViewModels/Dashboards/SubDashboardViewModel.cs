namespace Boek.Infrastructure.ViewModels.Dashboards
{
    public class SubDashboardViewModel<T>
    {
        public TimeLineViewModel timeLine { get; set; }
        public int Total { get; set; } = 0;
        public byte? Status { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public void UpdateTotal()
        {
            if (this.Data.Any())
                Total = Data.Count();
        }
    }
}