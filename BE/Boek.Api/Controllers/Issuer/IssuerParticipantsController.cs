using Boek.Infrastructure.Requests.Participants;
using Boek.Infrastructure.Responds;
using Boek.Infrastructure.ViewModels.Participants;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using Boek.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Boek.Core.Constants;

namespace Boek.Api.Controllers.Issuer
{
    [Route("api/issuer/participants")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Issuer))]
    public class IssuerParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public IssuerParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        /// <summary>
        /// Get all participants by issuer
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<BaseResponsePagingModel<ParticipantViewModel>> GetParticipants([FromQuery] ParticipantRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _participantService.GetParticipantsByIssuer(filter, paging);
        }

        /// <summary>
        /// Get a participant by id by issuer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> GetParticipantById(int id)
        {
            return _participantService.GetParticipantByIdByIssuer(id);
        }

        /// <summary>
        /// Create a participant by issuer
        /// </summary>
        /// <param name="appliedParticipant"></param>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> ApplyParticipant([FromBody] ApplyParticipantRequestModel appliedParticipant)
        {
            var result = _participantService.ApplyParticipant(appliedParticipant);
            _participantService.SendCheckingIssuerRequestNotification(result);
            return result;
        }

        /// <summary>
        /// Update a participant by issuer
        /// </summary>
        /// <param name="updatedParticipant"></param>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateParticipant([FromBody] UpdateParticipantRequestModel updatedParticipant)
        {
            return _participantService.UpdateParticipant(updatedParticipant, false);
        }

        #region Update statuses
        /// <summary>
        /// Update accept status of a participant by issuer
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("acceptance/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateAcceptedParticipant(int id)
        {
            var result = _participantService.UpdateAcceptedParticipant(id);
            _participantService.SendParticipantNotification(result);
            return result;
        }

        /// <summary>
        /// Update reject status of a participant by issuer
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("rejection/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ISSUER_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateRejectedInvitationParticipant(int id)
        {
            var result = _participantService.UpdateRejectedInvitationParticipant(id);
            _participantService.SendParticipantNotification(result);
            return result;
        }
        #endregion
    }
}
