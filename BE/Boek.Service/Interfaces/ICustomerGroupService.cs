using Boek.Infrastructure.Requests.CustomerGroups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerGroups;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICustomerGroupService
    {
        /// <summary>
        /// Get all customer groups(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CustomerGroupViewModel>
        GetCustomerGroups(
            CustomerGroupRequestModel filter, PagingModel paging
        );

        /// <summary>
        /// Get customer' groups
        /// </summary>
        /// <returns></returns>
        OwnedCustomerGroupViewModel GetCustomerGroupsByCustomer();

        /// <summary>
        /// Get a customer group detail by admin (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of a customer group if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched customer group
        /// </exception>
        CustomerGroupViewModel GetCustomerGroupById(int id);

        /// <summary>
        /// Create a customer group (<paramref name="createCustomerGroup"/>)
        /// </summary>
        /// <param name="createCustomerGroup"></param>
        /// <returns>If a customer group is valid, it returns a created customer group's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched customer
        /// 2. Throw a ErrorResponse if there is no matched group
        /// 3. Throw a ErrorResponse if there is a duplicated customer group
        /// </exception>
        CustomerGroupViewModel
        CreateCustomerGroup(
            CreateCustomerGroupRequestModel createCustomerGroup
        );

        /// <summary>
        ///  Delete a customer group (<paramref name="GroupId"/>)
        /// </summary>
        /// <param name="GroupId"></param>
        /// <returns>If a customer group is valid, then it returns the result of deleted customer group</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched customer group.
        /// 2. Throw a ErrorResponse if deleting an customer group is failed.
        /// </exception>
        CustomerGroupViewModel DeleteCustomerGroupById(int GroupId);
    }
}
