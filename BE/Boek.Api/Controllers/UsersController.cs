using Boek.Infrastructure.Requests.Users;
using Boek.Infrastructure.Requests.Users.Customers;
using Boek.Infrastructure.Requests.Users.Issuers;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Infrastructure.ViewModels.Users.Customers;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BaseResponsePagingModel<MultiUserViewModel>> GetUsers([FromQuery] UserRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _userService.GetUsers(filter, paging);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="WithAddressDetail"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUserById(Guid id, bool WithAddressDetail = false)
        {
            return _userService.GetUserById(id, WithAddressDetail);
        }

        /// <summary>
        /// Get login user info
        /// </summary>
        /// <returns></returns>
        [HttpGet("me")]
        [Authorize]
        public ActionResult<object> GetCurrentUser()
        {
            return _userService.GetCurrentLoginUser();
        }

        /// <summary>
        /// Create a customer
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CreateCustomerRequestModel createCustomer)
        {
            var result = (BaseResponseModel<LoginViewModel>)await _userService.CreateCustomer(createCustomer);
            if (result == null)
            {
                return BadRequest();
            }
            if (!result.Status.Success)
            {
                return BadRequest(result.Status.Message);
            }
            return Ok(result);
        }

        /// <summary>
        /// Update a staff
        /// </summary>
        /// <param name="updateUser"></param>
        [HttpPut("staff")]
        [Authorize(Roles = nameof(BoekRole.Staff))]
        public ActionResult<UserViewModel> UpdateStaff([FromBody] UpdateUserRequestModel updateUser)
        {
            return _userService.UpdateUser(updateUser);
        }

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="updateCustomer"></param>
        [HttpPut("customer")]
        [Authorize(Roles = nameof(BoekRole.Customer))]
        public ActionResult<CustomerUserViewModel> UpdateCustomer([FromBody] UpdateCustomerRequestModel updateCustomer)
        {
            return _userService.UpdateCustomerProfile(updateCustomer);
        }

        /// <summary>
        /// Update an issuer
        /// </summary>
        /// <param name="updateIssuer"></param>
        [HttpPut("issuer")]
        [Authorize(Roles = nameof(BoekRole.Issuer))]
        public ActionResult<IssuerViewModel> UpdateIssuer([FromBody] UpdateIssuerRequestModel updateIssuer)
        {
            return _userService.UpdateIssuerProfile(updateIssuer);
        }
        #endregion
    }
}
