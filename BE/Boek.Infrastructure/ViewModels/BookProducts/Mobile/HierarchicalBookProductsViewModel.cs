namespace Boek.Infrastructure.ViewModels.BookProducts.Mobile
{
    public class HierarchicalBookProductsViewModel
    {
        public int? CampaignId { get; set; }
        public string Title { get; set; }
        public List<SubHierarchicalBookProductsViewModel> subHierarchicalBookProducts { get; set; }
    }
}