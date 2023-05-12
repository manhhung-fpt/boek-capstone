using Boek.Infrastructure.Requests.Authentication;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Users;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Login
        /// <summary>
        /// Login with firebase
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult> LoginWithEmail([FromBody] LoginByFireBaseTokenRequest request)
        {
            var result = (BaseResponseModel<LoginViewModel>)await _userService.LoginByEmail(request.IdToken, request.FcmToken);
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
        #endregion
    }
}
