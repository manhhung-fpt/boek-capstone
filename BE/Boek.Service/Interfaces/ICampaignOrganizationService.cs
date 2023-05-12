using Boek.Infrastructure.Requests.CampaignOrganizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignOrganizations;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICampaignOrganizationService
    {
        /// <summary>
        /// Get all campaign organizations(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignOrganizationViewModel> GetCampaignOrganizations(CampaignOrganizationRequestModel filter, PagingModel paging);

        /// <summary>
        /// Get a campaign organization detail by admin (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of a campaign organization if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign organization
        /// </exception>
        CampaignOrganizationViewModel GetCampaignOrganizationById(int id);

        /// <summary>
        /// Create a campaign organization (<paramref name="createCampaignOrganization"/>)
        /// </summary>
        /// <param name="createCampaignOrganization"></param>
        /// <returns>If a campaign organization is valid, it returns a created campaign organization's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign
        /// 2. Throw a ErrorResponse if there is no matched organization
        /// 3. Throw a ErrorResponse if there is a duplicated campaign organization
        /// </exception>
        CampaignOrganizationViewModel CreateCampaignOrganization(CreateCampaignOrganizationRequestModel createCampaignOrganization);

        /// <summary>
        ///  Delete a campaign organization (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If a campaign organization is valid, then it returns the result of deleted campaign organization</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign organization.
        /// 2. Throw a ErrorResponse if deleting an campaign organization is failed.
        /// </exception>
        CampaignOrganizationViewModel DeleteCampaignOrganizationById(int id);
    }
}
