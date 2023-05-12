using Boek.Infrastructure.Requests.CampaignGroups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignGroups;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICampaignGroupService {
        /// <summary>
        /// Get all campaign groups(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignGroupViewModel> GetCampaignGroups(CampaignGroupRequestModel filter, PagingModel paging);

        /// <summary>
        /// Get a campaign group detail by admin (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of a campaign group if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign group
        /// </exception>
        CampaignGroupViewModel GetCampaignGroupById(int id);

        /// <summary>
        /// Create a campaign group (<paramref name="createCampaignGroup"/>)
        /// </summary>
        /// <param name="createCampaignGroup"></param>
        /// <returns>If a campaign group is valid, it returns a created campaign group's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign
        /// 2. Throw a ErrorResponse if there is no matched group
        /// 3. Throw a ErrorResponse if there is a duplicated campaign group
        /// </exception>
        CampaignGroupViewModel CreateCampaignGroup(CreateCampaignGroupRequestModel createCampaignGroup);

        /// <summary>
        ///  Delete a campaign group (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If a campaign group is valid, then it returns the result of deleted campaign group</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign group.
        /// 2. Throw a ErrorResponse if deleting an campaign group is failed.
        /// </exception>
        CampaignGroupViewModel DeleteCampaignGroupById(int id);
    }
}
