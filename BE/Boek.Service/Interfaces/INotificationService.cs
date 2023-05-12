using Boek.Infrastructure.Requests.Notifications;

namespace Boek.Service.Interfaces
{
    public interface INotificationService
    {
        #region Book Product
        void PushCheckingBookProductNotification(NotificationRequestModel notification);
        void PushDoneCheckingBookProductNotification(NotificationRequestModel notification);
        #endregion

        #region Participant
        void PushCheckingAdminInvitationNotification(List<NotificationRequestModel> notifications);
        void PushCheckingIssuerRequestNotification(NotificationRequestModel notification);
        void PushParticipantNotification(NotificationRequestModel notification);
        #endregion

        #region Order
        void PushCancelledOrderByCustomerNotification(NotificationRequestModel notification);
        void PushCancelledOrderByIssuerNotification(NotificationRequestModel notification);
        void PushNewOrderNotification(List<NotificationRequestModel> notifications);
        #endregion
    }
}