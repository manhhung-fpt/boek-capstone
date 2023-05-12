namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue
{
    public class RevenueViewModel<T>
    {
        public T Data { get; set; }
        public decimal? Total { get; set; }
    }
}