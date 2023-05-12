using Boek.Infrastructure.Requests.Participants;
using Boek.Infrastructure.ViewModels.Participants;
using Boek.Service.Commons;
using Boek.Infrastructure.Responds;

namespace Boek.Service.Interfaces
{
    public interface IParticipantService
    {
        #region Update and Delete
        ParticipantViewModel UpdateParticipant(UpdateParticipantRequestModel updatedParticipant, bool isAdmin = true);

        ParticipantViewModel DeleteParticipant(int id);
        #endregion

        #region Gets
        BaseResponsePagingModel<ParticipantViewModel> GetParticipants(ParticipantRequestModel filter, PagingModel paging);
        BaseResponsePagingModel<ParticipantViewModel> GetParticipantsByIssuer(ParticipantRequestModel filter, PagingModel paging);
        ParticipantViewModel GetParticipantById(int participantId);
        ParticipantViewModel GetParticipantByIdByIssuer(int participantId);
        #endregion

        #region Creates
        //Admin
        IEnumerable<ParticipantViewModel> CreateParticipants(InviteParticipantRequestModel invitedParticipant);
        //Issuer
        ParticipantViewModel ApplyParticipant(ApplyParticipantRequestModel appliedParticipant);
        #endregion

        #region Updates
        //Admin
        ParticipantViewModel UpdateApprovedParticipant(int id);
        ParticipantViewModel UpdateRejectedRequestParticipant(int id);
        ParticipantViewModel UpdateCancelledParticipant(int id);
        ParticipantViewModel UpdateCancelledStartDueDateParticipant(int id);

        //Issuer
        ParticipantViewModel UpdateAcceptedParticipant(int id);
        ParticipantViewModel UpdateRejectedInvitationParticipant(int id);

        #endregion

        #region Notification
        /// <summary>
        /// Send checking issuer's request notification
        /// </summary>
        /// <param name="participant"></param>
        void SendCheckingIssuerRequestNotification(ParticipantViewModel participant);
        /// <summary>
        /// Send checking admin's invitation notifications
        /// </summary>
        /// <param name="participants"></param>
        void SendCheckingAdminInvitationNotifications(IEnumerable<ParticipantViewModel> participants);
        /// <summary>
        /// Send participant status notification
        /// </summary>
        /// <param name="participant"></param>
        void SendParticipantNotification(ParticipantViewModel participant);

        #endregion
    }
}