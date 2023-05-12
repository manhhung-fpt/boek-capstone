using Boek.Infrastructure.Requests.CampaignStaffs;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CampaignStaffs;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICampaignStaffService
    {
        #region Gets
        /// <summary>
        /// Get authors
        /// (<paramref name="filter"/>,<paramref name="paging"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CampaignStaffViewModel> GetCampaignStaffs(CampaignStaffRequestModel filter, PagingModel paging);
        /// <summary>
        /// Get an campaign staff by id
        /// (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign staff.
        /// </exception>
        /// <returns></returns>
        CampaignStaffViewModel GetCampaignStaffById(int id);
        /// <summary>
        /// Get staff's campaigns by staff id (<paramref name="StaffId"/>)
        /// </summary>
        /// <param name="StaffId"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign staff.
        /// </exception>
        /// <returns></returns>
        StaffCampaignsViewModel GetStaffCampaignsByStaffId();
        /// <summary>
        /// Get campaign's staffs by campaign id (<paramref name="CampaignId"/>)
        /// </summary>
        /// <param name="CampaignId"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched campaign staff.
        /// </exception>
        /// <returns></returns>
        CampaignStaffsViewModel GetCampaignStaffsByCampaignId(int CampaignId);

        IEnumerable<UserViewModel> GetUnattendedCampaignStaffs(int CampaignId);
        #endregion

        #region Updates
        /// <summary>
        /// Update an campaign staff (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an campaign staff is valid, then it returns the result of updated campaign staff</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign staff.
        /// 2. Throw a ErrorResponse if there is a duplicated campaign staff's name.
        /// 3. Throw a ErrorResponse if updating an campaign staff is failed.
        /// </exception>
        BasicCampaignStaffViewModel UpdateCampaignStaffStatusIntoAttendedStatus(int id);
        /// <summary>
        /// Update an campaign staff (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an campaign staff is valid, then it returns the result of updated campaign staff</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched campaign staff.
        /// 2. Throw a ErrorResponse if there is a duplicated campaign staff's name.
        /// 3. Throw a ErrorResponse if updating an campaign staff is failed.
        /// </exception>
        BasicCampaignStaffViewModel UpdateCampaignStaffStatusIntoUnAttendedStatus(int id);
        #endregion

        #region Create
        /// <summary>
        /// Create an campaign staff (<paramref name="createdCampaignStaff"/>)
        /// </summary>
        /// <param name="createdCampaignStaff"></param>
        /// <returns>If an campaign staff is valid, then it returns the result of created campaign staff</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated campaign staff's name.
        /// 2. Throw a ErrorResponse if creating an campaign staff is failed.
        /// </exception>
        List<BasicCampaignStaffViewModel> CreateCampaignStaffs(CreateCampaignStaffRequestModel createdCampaignStaff);
        #endregion
    }
}