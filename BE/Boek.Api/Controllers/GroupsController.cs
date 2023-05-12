using Boek.Infrastructure.Requests.CustomerGroups;
using Boek.Infrastructure.Requests.Groups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerGroups;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IGroupService _groupService;

        private readonly ICustomerGroupService
            _customerGroupService;

        public GroupsController(IGroupService groupService, ICustomerGroupService customerGroupService)
        {
            _groupService = groupService;
            _customerGroupService = customerGroupService;
        }
        #endregion

        #region Groups
        /// <summary>
        /// Get all groups
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <param name="WithCustomers"></param>
        /// <param name="WithCampaigns"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<GroupViewModel>> GetGroups([FromQuery] GroupRequestModel filter, [FromQuery] PagingModel paging,
        [FromQuery] bool WithCustomers = false, [FromQuery] bool WithCampaigns = false)
        {
            return _groupService.GetGroups(filter, paging, WithCustomers, WithCampaigns);
        }

        /// <summary>
        /// Get a group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<BasicGroupViewModel> GetGroupById(int id)
        {
            return _groupService.GetGroupById(id);
        }
        #endregion

        #region Customer Groups
        /// <summary>
        /// Get a customer's following groups
        /// </summary>
        /// <returns></returns>
        [HttpGet("customer")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<OwnedCustomerGroupViewModel> GetCustomerGroupsByCustomer()
        {
            return _customerGroupService.GetCustomerGroupsByCustomer();
        }

        /// <summary>
        /// Follow a group
        /// </summary>
        /// <param name="createCustomerGroup"></param>
        /// <returns></returns>
        [HttpPost("customer")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<CustomerGroupViewModel> CreateCustomerGroup([FromBody] CreateCustomerGroupRequestModel createCustomerGroup)
        {
            return _customerGroupService.CreateCustomerGroup(createCustomerGroup);
        }

        /// <summary>
        /// Unfollow a group
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        [HttpDelete("customer/{groupid}")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<CustomerGroupViewModel> DeleteGroup(int groupid)
        {
            return _customerGroupService.DeleteCustomerGroupById(groupid);
        }
        #endregion
    }
}
