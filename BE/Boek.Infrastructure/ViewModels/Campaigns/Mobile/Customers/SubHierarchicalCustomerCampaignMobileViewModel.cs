namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers
{
    public class SubHierarchicalCustomerCampaignMobileViewModel
    {
        public string SubTitle { get; set; }
        public int? OrganizationId { get; set; }
        public int? GroupId { get; set; }
        public byte? Status { get; set; }
        public byte? Format { get; set; }
        public List<CampaignViewModel> campaigns { get; set; }
    }
}