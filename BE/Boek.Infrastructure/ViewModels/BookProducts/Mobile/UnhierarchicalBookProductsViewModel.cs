namespace Boek.Infrastructure.ViewModels.BookProducts.Mobile
{
    public class UnhierarchicalBookProductsViewModel
    {
        public int? CampaignId { get; set; }
        public string Title { get; set; }
        public List<MobileBookProductsViewModel> BookProducts { get; set; }
    }
}