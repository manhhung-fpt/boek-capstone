namespace Boek.Infrastructure.ViewModels.Dashboards
{
    public class SummaryViewModel
    {
        public int Id { get; set; }
        public int QuantityOfTitle { get; set; }
        public string Title { get; set; }
        public int QuantityOfSubTitle { get; set; }
        public byte? Status { get; set; } = null;
        public string StatusName { get; set; }
        public string SubTitle { get; set; }
    }
}