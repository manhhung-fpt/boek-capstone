using Boek.Infrastructure.Requests.CustomerOrganizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerOrganizations;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface ICustomerOrganizationService
    {
        /// <summary>
        /// Get all customer organizations(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<CustomerOrganizationViewModel> GetCustomerOrganizations(CustomerOrganizationRequestModel filter, PagingModel paging);

        /// <summary>
        /// Get customer' organizations
        /// </summary>
        /// <returns></returns>
        OwnedCustomerOrganizationViewModel GetCustomerOrganizationsByCustomer();

        /// <summary>
        /// Get a customer organization detail by admin (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of a customer organization if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched customer organization
        /// </exception>
        CustomerOrganizationViewModel GetCustomerOrganizationById(int id);

        /// <summary>
        /// Create a customer organization (<paramref name="createCustomerOrganization"/>)
        /// </summary>
        /// <param name="createCustomerOrganization"></param>
        /// <returns>If a customer organization is valid, it returns a created customer organization's detail</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched customer
        /// 2. Throw a ErrorResponse if there is no matched organization
        /// 3. Throw a ErrorResponse if there is a duplicated customer organization
        /// </exception>
        CustomerOrganizationViewModel CreateCustomerOrganization(CreateCustomerOrganizationRequestModel createCustomerOrganization);

        /// <summary>
        ///  Delete a customer organization (<paramref name="OrganizationId"/>)
        /// </summary>
        /// <param name="OrganizationId"></param>
        /// <returns>If a customer organization is valid, then it returns the result of deleted customer organization</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched customer organization.
        /// 2. Throw a ErrorResponse if deleting an customer organization is failed.
        /// </exception>
        CustomerOrganizationViewModel DeleteCustomerOrganizationById(int OrganizationId);
    }
}
