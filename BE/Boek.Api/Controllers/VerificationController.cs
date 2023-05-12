using Boek.Infrastructure.Requests.Verifications;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerificationController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IVerificationService _verificationService;

        public VerificationController(IVerificationService verificationService)
        {
            _verificationService = verificationService;
        }
        #endregion

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("email")]
        public ActionResult SendEmail([FromBody] EmailRequestModel request)
        {
            _verificationService.SendEmail(request);
            return Ok();
        }

        /// <summary>
        /// Send OTP by email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("otp")]
        public ActionResult SendOTPEmail([FromBody] EmailRequestModel request)
        {
            _verificationService.SendOTPEmail(request);
            return Ok();
        }
        /// <summary>
        /// Re-send OTP by email
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("re-otp")]
        public ActionResult ResendOTPEmail([FromBody] EmailRequestModel request)
        {
            _verificationService.ResendOTPEmail(request);
            return Ok();
        }
        /// <summary>
        /// Validate OTP by email
        /// </summary>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpPost("otp-validation")]
        public ActionResult<string> ValidateOTPEmail([FromBody] OtpRequestModel otp)
        {
            return _verificationService.ValidateOTPEmail(otp);
        }
    }
}