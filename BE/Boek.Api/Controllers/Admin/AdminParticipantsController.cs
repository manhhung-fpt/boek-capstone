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

namespace Boek.Api.Controllers.Admin
{
    [Route("api/admin/participants")]
    [ApiController]
    [Authorize(Roles = nameof(BoekRole.Admin))]
    public class AdminParticipantsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly IParticipantService _participantService;
        public AdminParticipantsController(IParticipantService participantService)
        {
            _participantService = participantService;
        }
        #endregion

        /// <summary>
        /// Get all participants by admin
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<BaseResponsePagingModel<ParticipantViewModel>> GetParticipants([FromQuery] ParticipantRequestModel filter,
            [FromQuery] PagingModel paging)
        {
            return _participantService.GetParticipants(filter, paging);
        }

        /// <summary>
        /// Get a participant by id by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> GetParticipantById(int id)
        {
            return _participantService.GetParticipantById(id);
        }

        /// <summary>
        /// Create a participant by admin
        /// </summary>
        /// <param name="invitedParticipants"></param>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<IEnumerable<ParticipantViewModel>> CreateParticipants([FromBody] InviteParticipantRequestModel invitedParticipants)
        {
            var result = _participantService.CreateParticipants(invitedParticipants).ToList();
            _participantService.SendCheckingAdminInvitationNotifications(result);
            return result;
        }

        /// <summary>
        /// Update a participant by admin
        /// </summary>
        /// <param name="updatedParticipant"></param>
        [HttpPut]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateParticipant([FromBody] UpdateParticipantRequestModel updatedParticipant)
        {
            return _participantService.UpdateParticipant(updatedParticipant);
        }

        /// <summary>
        /// Delete a participant by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> DeletedParticipant(int id)
        {
            return _participantService.DeleteParticipant(id);
        }

        #region Update Statuses
        /// <summary>
        /// Update reject status of a participant by admin
        /// </summary>
        /// /// <param name="id"></param>
        [HttpPut("rejection/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateRejectedRequestParticipant(int id)
        {
            var result = _participantService.UpdateRejectedRequestParticipant(id);
            _participantService.SendParticipantNotification(result);
            return result;
        }

        /// <summary>
        /// Update approve status of a participant by admin
        /// </summary>
        /// /// <param name="id"></param>
        [HttpPut("approval/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateApprovedParticipant(int id)
        {
            var result = _participantService.UpdateApprovedParticipant(id);
            _participantService.SendParticipantNotification(result);
            return result;
        }

        /// <summary>
        /// Update cancel status of a participant by admin
        /// </summary>
        /// <param name="id"></param>
        [HttpPut("cancellation/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateCancelledParticipant(int id)
        {
            var result = _participantService.UpdateCancelledParticipant(id);
            _participantService.SendParticipantNotification(result);
            return result;
        }

        /// <summary>
        /// Update cancel due date status of a participant by admin
        /// </summary>
        /// /// <param name="id"></param>
        [HttpPut("cancellation/due-start-date/{id}")]
        [SwaggerOperation(Tags = new[] { MessageConstants.SWAGGER_OPERATION_ADMIN_PARTICIPANT })]
        public ActionResult<ParticipantViewModel> UpdateCancelledStartDueDateParticipant(int id)
        {
            return _participantService.UpdateCancelledStartDueDateParticipant(id);
        }
        #endregion
    }
}