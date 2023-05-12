using Boek.Infrastructure.Requests.CampaignCommissions;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignCommissions;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICampaignCommissionService
    {
        /// <summary>
        /// Get all campaign commissions (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignCommissionViewModel> GetCampaignsCommissions(CampaignCommissionRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get a campaign commission's detail by id
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign commission.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign commission. Otherwise, it returns not found message.
        /// </returns>
        CampaignCommissionViewModel GetCampaignCommissionById(int id);
        /// <summary>
        /// Create a campaign commission (<paramref name="createCampaignCommission"/>)
        /// </summary>
        /// <param name="createCampaignCommission"></param>
        /// <returns>If a campaign is valid, then it returns the result of created campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign's commission.
        /// 2. Throw a ErrorResponse if not found a matched genre or a matched campaign
        /// 3. Throw a ErrorResponse if genre is invalid
        /// 4. Throw a ErrorResponse if campaign is started or over
        /// 5. Throw a ErrorResponse if creating a campaign is failed.
        /// </exception>
        CampaignCommissionViewModel CreateCampaignCommission(CreateCampaignCommissionRequestModel createCampaignCommission);
        /// <summary>
        /// Update a campaign commission (<paramref name="updateCampaignCommission"/>)
        /// </summary>
        /// <param name="updateCampaignCommission"></param>
        /// <returns>If a campaign is valid, then it returns the result of updated campaign commission</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if not found a matched genre or a matched campaign
        /// 2. Throw a ErrorResponse if genre is invalid
        /// 3. Throw a ErrorResponse if campaign is started or over
        /// 4. Throw a ErrorResponse if updating a campaign commission is failed.
        /// </exception>
        CampaignCommissionViewModel UpdateCampaignCommission(UpdateCampaignCommissionRequestModel updateCampaignCommission);
        /// <summary>
        /// Delete a campaign commission (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an campaign commission is valid, then it returns the result of deleted campaign commission</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign commission.
        /// 2. Throw a ErrorResponse if campaign is started or over
        /// 3. Throw a ErrorResponse if there is book product having same genre with the deleted one
        /// 4. Throw a ErrorResponse if deleting an campaign commission is failed.
        /// </exception>
        CampaignCommissionViewModel DeleteCampaignCommission(int id);
    }
}
