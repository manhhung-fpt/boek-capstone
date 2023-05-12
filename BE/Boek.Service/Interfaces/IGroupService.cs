using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Groups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IGroupService
    {
        /// <summary>
        /// Get all groups(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<GroupViewModel> GetGroups(GroupRequestModel filter, PagingModel paging, bool WithCustomers = false, bool WithCampaigns = false);

        /// <summary>
        /// Get an group by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of an group if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched group
        /// </exception>
        BasicGroupViewModel GetGroupById(int id);

        /// <summary>
        /// Update an group (<paramref name="updateGroup"/>)
        /// </summary>
        /// <param name="updateGroup"></param>
        /// <returns>If an group is valid, then it returns the result of updated group</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched group.
        /// 2. Throw a ErrorResponse if there is a duplicated group's name.
        /// 3. Throw a ErrorResponse if updated status is false, but this group is linked to other information.
        /// 4. Throw a ErrorResponse if updating an group is failed.
        /// </exception>
        BasicGroupViewModel UpdateGroup(UpdateGroupRequestModel updateGroup);

        /// <summary>
        /// Create an group (<paramref name="createGroup"/>)
        /// </summary>
        /// <param name="createGroup"></param>
        /// <returns>If an group is valid, then it returns the result of created group</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated group's name.
        /// 2. Throw a ErrorResponse if creating an group is failed.
        /// </exception>
        BasicGroupViewModel CreateGroup(CreateGroupRequestModel createGroup);

        /// <summary>
        /// Check if group's name is duplicated
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Group CheckDuplicatedGroupName(string name);
    }
}
