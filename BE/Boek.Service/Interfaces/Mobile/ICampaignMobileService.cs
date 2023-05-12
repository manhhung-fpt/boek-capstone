using Boek.Infrastructure.Requests.Campaigns.Mobile;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile;
using Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces.Mobile
{
    public interface ICampaignMobileService
    {
        /// <summary>
        /// Get a mobile campaign by id (<paramref name="id"/>) 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns></returns>
        CampaignMobileViewModel GetCampaignMobileById(int id);
        /// <summary>
        /// Get all campaigns (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignViewModel> GetCampaigns(CampaignMobileRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get a staff's campaigns (<paramref name="filter"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<HierarchicalStaffCampaignsViewModel> GetStaffCampaigns(StaffCampaignMobileRequestModel filter);
        /// <summary>
        /// Get a customer's mobile campaigns
        /// </summary>
        /// <returns></returns>
        CustomerCampaignMobileViewModel GetCustomerCampaigns(int size = 5);
    }
}