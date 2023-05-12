using Boek.Core.Entities;
using Boek.Infrastructure.Requests.Organizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Service.Commons;

namespace Boek.Service.Interfaces
{
    public interface IOrganizationService
    {
        /// <summary>
        /// Get all organizations(<paramref name="filter"/>, <paramref name="paging"/> )
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        BaseResponsePagingModel<OrganizationViewModel> GetOrganizations(OrganizationRequestModel filter, PagingModel paging, OrganizationDetailRequestModel organizationDetail);

        /// <summary>
        /// Get an organization by id (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return the detail of an organization if found a matched result. Otherwise, it returns a not found message</returns>
        /// <exception cref="ErrorResponse">
        /// Throw a ErrorResponse if there is no matched organization
        /// </exception>
        OrganizationViewModel GetOrganizationById(int id);

        /// <summary>
        /// Update an organization (<paramref name="updateOrganization"/>)
        /// </summary>
        /// <param name="updateOrganization"></param>
        /// <returns>If an organization is valid, then it returns the result of updated organization</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched organization.
        /// 2. Throw a ErrorResponse if there is a duplicated organization's name.
        /// 3. Throw a ErrorResponse if updating an organization is failed.
        /// </exception>
        OrganizationViewModel UpdateOrganization(UpdateOrganizationRequestModel updateOrganization);

        /// <summary>
        /// Create an organization (<paramref name="createOrganization"/>)
        /// </summary>
        /// <param name="createOrganization"></param>
        /// <returns>If an organization is valid, then it returns the result of created organization</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is a duplicated organization's name.
        /// 2. Throw a ErrorResponse if creating an organization is failed.
        /// </exception>
        OrganizationViewModel CreateOrganization(CreateOrganizationRequestModel createOrganization);

        /// <summary>
        /// Check if organization's name is duplicated
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Organization CheckDuplicatedOrganizationName(string name);

        /// <summary>
        /// Delete an organization (<paramref name="id"/>)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If an organization is valid, then it returns the result of deleted organization</returns>
        /// <exception cref="ErrorResponse">
        /// 1. Throw a ErrorResponse if there is no matched organization.
        /// 2. Throw a ErrorResponse if this organization is linked to other information.
        /// 3. Throw a ErrorResponse if deleting an organization is failed.
        /// </exception>
        BasicOrganizationViewModel DeleteOrganization(int id);
    }
}
