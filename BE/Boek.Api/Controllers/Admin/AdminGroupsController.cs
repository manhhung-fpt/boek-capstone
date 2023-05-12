using Boek.Core.Constants;
using Boek.Infrastructure.Requests.CustomerGroups;
using Boek.Infrastructure.Requests.Groups;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.CustomerGroups;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/groups")]
    [ApiController]
    public class AdminGroupsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IGroupService _groupService;

        private readonly ICustomerGroupService _customerGroupService;

        public AdminGroupsController(IGroupService groupService,
            ICustomerGroupService customerGroupService)
        {
            _groupService = groupService;
            _customerGroupService = customerGroupService;
        }
        #endregion

        #region Groups
        /// <summary>
        /// Create a group by admin
        /// </summary>
        /// <param name="createGroup"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GROUP })]
        public ActionResult<BasicGroupViewModel> CreateGroup([FromBody] CreateGroupRequestModel createGroup)
        {
            return _groupService.CreateGroup(createGroup);
        }

        /// <summary>
        /// Update a group by admin
        /// </summary>
        /// <param name="updateGroup"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GROUP })]
        public ActionResult<BasicGroupViewModel> UpdateGroup([FromBody] UpdateGroupRequestModel updateGroup)
        {
            return _groupService.UpdateGroup(updateGroup);
        }
        #endregion

        #region Customer Groups
        /// <summary>
        /// Get all customer groups
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GROUP })]
        public ActionResult<BaseResponsePagingModel<CustomerGroupViewModel>> GetCustomerGroups([FromQuery] CustomerGroupRequestModel filter, [FromQuery] PagingModel paging)
        {
            return _customerGroupService.GetCustomerGroups(filter, paging);
        }

        /// <summary>
        /// Get a customer group by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_GROUP })]
        public ActionResult<CustomerGroupViewModel> GetCustomerGroupById(int id)
        {
            return _customerGroupService.GetCustomerGroupById(id);
        }
        #endregion
    }
}
