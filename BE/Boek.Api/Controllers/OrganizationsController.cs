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

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IOrganizationService _organizationService;

        private readonly ICustomerOrganizationService
            _customerOrganizationService;

        public OrganizationsController(IOrganizationService organizationService, ICustomerOrganizationService customerOrganizationService)
        {
            _organizationService = organizationService;
            _customerOrganizationService = customerOrganizationService;
        }
        #endregion

        #region Organizations
        /// <summary>
        /// Get all organizations
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="organizationDetail"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<OrganizationViewModel>> GetOrganizations([FromQuery] OrganizationRequestModel filter, [FromQuery] PagingModel paging, [FromQuery] OrganizationDetailRequestModel organizationDetail)
        {
            return _organizationService.GetOrganizations(filter, paging, organizationDetail);
        }

        /// <summary>
        /// Get an organization by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<OrganizationViewModel> GetOrganizationById(int id)
        {
            return _organizationService.GetOrganizationById(id);
        }
        #endregion

        #region Customer Organizations
        /// <summary>
        /// Get a customer's following organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet("customer")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<OwnedCustomerOrganizationViewModel> GetCustomerOrganizationsByCustomer()
        {
            return _customerOrganizationService.GetCustomerOrganizationsByCustomer();
        }
        #endregion
    }
}
