using Boek.Infrastructure.Requests.CustomerOrganizations;
using Boek.Infrastructure.Requests.Organizations;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerOrganizations;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers
{
    [Route("api/admin/organizations")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminOrganizationsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrganizationService _organizationService;

        private readonly ICustomerOrganizationService _customerOrganizationService;

        public AdminOrganizationsController(
            IOrganizationService organizationService,
            ICustomerOrganizationService customerOrganizationService)
        {
            _organizationService = organizationService;
            _customerOrganizationService = customerOrganizationService;
        }
        #endregion

        #region Organizations
        /// <summary>
        /// Create an organization by admin
        /// </summary>
        /// <param name="createOrganization"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORGANIZATION })]
        public ActionResult<OrganizationViewModel> CreateOrganization([FromBody] CreateOrganizationRequestModel createOrganization)
        {
            return _organizationService.CreateOrganization(createOrganization);
        }

        /// <summary>
        /// Update an organization by admin
        /// </summary>
        /// <param name="updateOrganization"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORGANIZATION })]
        public ActionResult<OrganizationViewModel> UpdateOrganization([FromBody] UpdateOrganizationRequestModel updateOrganization)
        {
            return _organizationService.UpdateOrganization(updateOrganization);
        }

        /// <summary>
        /// Delete an organization by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORGANIZATION })]
        [HttpDelete("{id}")]
        public ActionResult<BasicOrganizationViewModel> DeleteOrganization(int id)
        {
            return _organizationService.DeleteOrganization(id);
        }
        #endregion

        #region Customer Organizations
        /// <summary>
        /// Get all customer organizations
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORGANIZATION })]
        public ActionResult<BaseResponsePagingModel<CustomerOrganizationViewModel>> GetCustomerOrganizations([FromQuery] CustomerOrganizationRequestModel filter, [FromQuery] PagingModel paging)
        {
            return _customerOrganizationService
                .GetCustomerOrganizations(filter, paging);
        }

        /// <summary>
        /// Get a customer organization by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_ORGANIZATION })]
        public ActionResult<CustomerOrganizationViewModel> GetCustomerOrganizationById(int id)
        {
            return _customerOrganizationService.GetCustomerOrganizationById(id);
        }
        #endregion
    }
}
