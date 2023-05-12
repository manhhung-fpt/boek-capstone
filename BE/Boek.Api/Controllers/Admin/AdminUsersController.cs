using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Users;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/users")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminUsersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IUserService _userService;
        public AdminUsersController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Users
        /// <summary>
        /// Get users by admin
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_USER })]
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<MultiUserViewModel>> GetUsers([FromQuery] UserRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _userService.GetUsers(filter, paging);
        }

        /// <summary>
        /// Get a user by id by admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithAddressDetail"></param>
        /// <returns></returns>
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_USER })]
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUserById(Guid id, bool WithAddressDetail = false)
        {
            return _userService.GetUserById(id, WithAddressDetail);
        }

        /// <summary>
        /// Update a user by admin
        /// </summary>
        /// <param name="updateUser"></param>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_USER })]
        public ActionResult<UserViewModel> UpdateUser([FromBody] UpdateUserRequestModel updateUser)
        {
            return _userService.UpdateUserByAdmin(updateUser);
        }

        /// <summary>
        /// Create a user by admin
        /// </summary>
        /// <param name="createUser"></param>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_USER })]
        public async Task<ActionResult<UserViewModel>> CreateUser([FromBody] CreateUserRequestModel createUser)
        {
            _userService.CheckDuplicatedEmail(createUser.Email);
            var user = await _userService.CreateUser(createUser);
            return Ok(user);
        }
        #endregion
    }
}
