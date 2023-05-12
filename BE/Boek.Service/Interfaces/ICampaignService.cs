using Boek.Infrastructure.Requests.Campaigns;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICampaignService
    {
        #region Gets
        /// <summary>
        /// Get all campaigns (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignViewModel> GetCampaigns(CampaignRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get all campaigns by admin (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignViewModel> GetCampaignsByAdmin(CampaignRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get participated campaigns by issuer(<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignViewModel> GetCampaignsByIssuer(CampaignRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get other campaigns by issuer (<paramref name="filter"/>, <paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignViewModel> GetOtherCampaignsByIssuer(CampaignRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get unanticipated issuer (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> GetUnparticipatedIssuers(int id);
        /// <summary>
        /// Get a campaign by id
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign. Otherwise, it returns not found message.
        /// </returns>
        CampaignViewModel GetCampaignById(int id);
        /// <summary>
        /// Get a campaign detail by admin
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign. Otherwise, it returns not found message.
        /// </returns>
        CampaignViewModel GetCampaignByIdByAdmin(int id, bool WithAddressDetail = false);
        /// <summary>
        /// Get a campaign detail by issuer
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign. Otherwise, it returns not found message.
        /// </returns>
        CampaignViewModel GetCampaignByIdByIssuer(int id);
        /// <summary>
        /// Get the other campaign detail by issuer
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign. Otherwise, it returns not found message.
        /// </returns>
        CampaignViewModel GetOtherCampaignByIdByIssuer(int id);
        #endregion

        #region Creates
        /// <summary>
        /// Create an online campaign (<paramref name="createOnlineCampaign"/>)
        /// </summary>
        /// <param name="createOnlineCampaign"></param>
        /// <returns>If an online campaign is valid, then it returns the result of created online campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign's name.
        /// 2. Throw a ErrorResponse if creating an online campaign is failed.
        /// </exception>
        CampaignViewModel CreateOnlineCampaign(CreateOnlineCampaignRequestModel createOnlineCampaign);
        /// <summary>
        /// Create an offline campaign (<paramref name="createOfflineCampaign"/>)
        /// </summary>
        /// <param name="createOfflineCampaign"></param>
        /// <returns>If a campaign is valid, then it returns the result of created offline campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign's name.
        /// 2. Throw a ErrorResponse if creating an offline campaign is failed.
        /// </exception>
        CampaignViewModel CreateOfflineCampaign(CreateOfflineCampaignRequestModel createOfflineCampaign);
        #endregion

        #region Updates
        /// <summary>
        /// Update an online campaign (<paramref name="updateOnlineCampaign"/>)
        /// </summary>
        /// <param name="updateOnlineCampaign"></param>
        /// <returns>If an online campaign is valid, then it returns the result of updated online campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign's name.
        /// 2. Throw a ErrorResponse if updating an online campaign is failed.
        CampaignViewModel UpdateOnlineCampaign(UpdateOnlineCampaignRequestModel updateOnlineCampaign);
        /// <summary>
        /// Update an offline campaign (<paramref name="updateOfflineCampaign"/>)
        /// </summary>
        /// <param name="updateOfflineCampaign"></param>
        /// <returns>If an offline campaign is valid, then it returns the result of updated offline campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign's name.
        /// 2. Throw a ErrorResponse if updating an offline campaign is failed.
        CampaignViewModel UpdateOfflineCampaign(UpdateOfflineCampaignRequestModel updateOfflineCampaign);
        #endregion

        #region Cancels
        /// <summary>
        /// Cancel an online campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an online campaign is valid, then it returns the result of cancelled online campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if cancelling an online campaign is failed.
        CampaignViewModel CancelOnlineCampaign(int id);
        /// <summary>
        /// Cancel an offline campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an offline campaign is valid, then it returns the result of cancelled offline campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if cancelling an offline campaign is failed.
        CampaignViewModel CancelOfflineCampaign(int id);
        #endregion

        #region Postpones
        /// <summary>
        /// Postpone an online campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an online campaign is valid, then it returns the result of postponed online campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if postponing an online campaign is failed.
        CampaignViewModel PostponeOnlineCampaign(int id);
        /// <summary>
        /// Postpone an offline campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an offline campaign is valid, then it returns the result of postponed offline campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if postponing an offline campaign is failed.
        CampaignViewModel PostponeOfflineCampaign(int id);
        #endregion

        #region Restarts
        /// <summary>
        /// Restart a postponed online campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an online campaign is valid, then it returns the result of online campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if restarting an online campaign is failed.
        CampaignViewModel RestartOnlineCampaign(int id);
        /// <summary>
        /// Restart a postponed offline campaign (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an offline campaign is valid, then it returns the result of offline campaign</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign.
        /// 2. Throw a ErrorResponse because of invalid format.
        /// 3. Throw a ErrorResponse if the campaign is started or over.
        /// 4. Throw a ErrorResponse if restarting an offline campaign is failed.
        CampaignViewModel RestartOfflineCampaign(int id);
        #endregion

        #region Update Started Campaign
        /// <summary>
        /// Update the basic info of a started campaign
        /// </summary>
        /// <param name="updateCampaign"></param>
        /// <returns></returns>
        CampaignViewModel UpdateStartedCampaign(UpdateCampaignRequestModel updateCampaign);
        #endregion

        #region Campaign Schedules
        /// <summary>
        /// Get all campaign schedules (<param name="filter">, <paramref name="paging"/>, <param name="scheduleStatus">, <paramref name="WithOccurringProvinces"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="scheduleStatus"></param>
        /// <paramref name="WithOccurringProvinces"/></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignSchedulesViewModel> GetCampaignSchedules(CampaignRequestModel filter, PagingModel paging, List<byte?> scheduleStatus, bool WithOccurringProvinces = false);
        /// <summary>
        /// Get a campaign's schedules by id
        /// (<paramref name="id"/>, <param name="scheduleStatus">, <paramref name="WithOccurringProvinces"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="scheduleStatus"></param>
        /// <paramref name="WithOccurringProvinces"/></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign.
        /// </exception>
        /// <returns>
        /// It returns a matched campaign. Otherwise, it returns not found message.
        /// </returns>
        CampaignSchedulesViewModel GetCampaignSchedule(int id, List<byte?> scheduleStatus, bool WithOccurringProvinces = false);
        #endregion
    }
}
