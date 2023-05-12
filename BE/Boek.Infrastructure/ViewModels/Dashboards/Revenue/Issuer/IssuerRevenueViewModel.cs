namespace Boek.Infrastructure.ViewModels.Dashboards.Revenue.Issuer
{
    public class IssuerRevenueViewModel
    {
        public TimeLineViewModel timeLine { get; set; }
        public decimal? Revenue { get; set; } = 0;
    }
}