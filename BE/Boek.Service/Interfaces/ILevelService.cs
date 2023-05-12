using Boek.Infrastructure.Requests.Levels;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Levels;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ILevelService
    {
        /// <summary>
        /// Get authors
        /// (<paramref name="filter"/>,<paramref name="paging"/>,<paramref name="WithCustomers"/>)
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithBooks"></param>
        /// <returns>
        /// If a request includes WithCustomers, then it returns all authors with customers respectively. Otherwise, it returns just authors only.
        /// </returns>
        BaseResponsePagingModel<LevelViewModel> GetLevels(LevelRequestModel filter, PagingModel paging, bool WithCustomers = false);
        /// <summary>
        /// Get a level by id
        /// (<paramref name="id"/>,<paramref name="WithCustomers"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithCustomers"></param>
        /// /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched level.
        /// </exception>
        /// <returns>
        /// If a request includes WithCustomers, then it returns a matched level with customers. Otherwise, it returns just a matched level only.
        /// </returns>
        LevelViewModel GetLevelById(int id, bool WithCustomers = false);
        /// <summary>
        /// Update a level (<paramref name="updateLevel"/>)
        /// </summary>
        /// <param name="updateLevel"></param>
        /// <returns>If a level is valid, then it returns the result of updated level</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched level.
        /// 2. Throw a ErrorResponse if there is a duplicated level's name.
        /// 3. Throw a ErrorResponse if updating a level is failed.
        /// </exception>
        LevelViewModel UpdateLevel(UpdateLevelRequestModel updateLevel);
        /// <summary>
        /// Create a level (<paramref name="createLevel"/>)
        /// </summary>
        /// <param name="createLevel"></param>
        /// <returns>If a level is valid, then it returns the result of created level</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated level's name.
        /// 2. Throw a ErrorResponse if creating a level is failed.
        /// </exception>
        LevelViewModel CreateLevel(CreateLevelRequestModel createLevel);
        /// <summary>
        /// Convert customers' level by new level(<paramref name="OldLevelId"/>, <paramref name="NewLevelId"/>)
        /// </summary>
        /// <param name="OldLevelId"></param>
        /// <param name="NewLevelId"></param>
        /// <returns></returns>
        LevelViewModel ConvertCustomerLevelByNewLevel(int OldLevelId, int NewLevelId, bool DisableOldLevelStatus = false);
    }
}
