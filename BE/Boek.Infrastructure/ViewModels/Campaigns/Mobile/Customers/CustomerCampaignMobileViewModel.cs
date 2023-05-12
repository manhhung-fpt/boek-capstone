namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers
{
    public class CustomerCampaignMobileViewModel
    {
        public List<HierarchicalCustomerCampaignMobileViewModel> hierarchicalCustomerCampaigns { get; set; }
        public List<UnhierarchicalCustomerCampaignMobileViewModel> unhierarchicalCustomerCampaigns { get; set; }
    }
}