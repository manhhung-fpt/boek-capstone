namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers
{
    public class HierarchicalCustomerCampaignMobileViewModel
    {
        public string Title { get; set; }
        public List<SubHierarchicalCustomerCampaignMobileViewModel> subHierarchicalCustomerCampaigns { get; set; }
    }
}