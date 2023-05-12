using System.Net;
using Boek.Core.Constants;
using Boek.Core.Enums;
using Boek.Core.Extensions;
using Boek.Infrastructure.Requests.Notifications;
using Boek.Infrastructure.ViewModels.Notifications;
using Boek.Repository.Interfaces;
using Boek.Service.Commons;
using Boek.Service.Hubs;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Boek.Service.Services
{
    public class NotificationService : INotificationService
    {
        #region Fields and constructor
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NotificationService> _logger;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IServiceProvider services, ILogger<NotificationService> logger)
        {
            var scope = services.CreateScope();
            _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _hub = scope.ServiceProvider.GetRequiredService<IHubContext<NotificationHub>>();
            _logger = logger;
        }
        #endregion

        #region Notification

        #region Book Products
        public void PushCheckingBookProductNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.CheckingBookProduct, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_CHECKING_BOOK_PRODUCT}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }

        public void PushDoneCheckingBookProductNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.DoneCheckingBookProduct, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_DONE_CHECKING_BOOK_PRODUCT}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }
        #endregion

        #region Participants
        public void PushCheckingAdminInvitationNotification(List<NotificationRequestModel> notifications)
        {
            try
            {
                var responses = new List<NotificationViewModel>();
                foreach (var item in notifications)
                    responses.AddRange(ConvertRequestIntoResponse(item, NotificationType.ParticipantInvitation, MessageConstants.NOTI_MESS_URL));
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notifications);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_CHECKING_ADMIN_INVITATION}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }

        public void PushCheckingIssuerRequestNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.ParticipantRequest, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_CHECKING_ISSUER_REQUEST}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }

        public void PushParticipantNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.ParticipantStatus, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_PARTICIPANT_STATUS}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }
        #endregion

        #region Orders
        public void PushCancelledOrderByCustomerNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.CancelledOrder, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_ISSUER_CANCELLED_ORDER}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }

        public void PushCancelledOrderByIssuerNotification(NotificationRequestModel notification)
        {
            try
            {
                var responses = ConvertRequestIntoResponse(notification, NotificationType.CancelledOrder, MessageConstants.NOTI_MESS_URL);
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notification);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_CUSTOMER_CANCELLED_ORDER}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }

        public void PushNewOrderNotification(List<NotificationRequestModel> notifications)
        {
            try
            {
                var responses = new List<NotificationViewModel>();
                foreach (var item in notifications)
                    responses.AddRange(ConvertRequestIntoResponse(item, NotificationType.NewOrder, MessageConstants.NOTI_MESS_URL));
                SendNotiAsync(responses);
            }
            catch (Exception ex)
            {
                var result = ConvertErrorObjects(notifications);
                _logger.LogError($"{ErrorMessageConstants.NOTI_ERROR_NEW_ORDER}{ex.Message}", result.Any() ? result.ToArray() : null);
            }
        }
        #endregion

        #endregion

        #region Utils
        private List<NotificationViewModel> ConvertRequestIntoResponse(NotificationRequestModel request, NotificationType type, string url)
        {
            if (request == null)
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.NOTI_IDS_OR_ROLES
                });
            if (request.IsEmptyIdsAndRoles())
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.NOTI_IDS_OR_ROLES
                });
            List<NotificationViewModel> result = null;
            var users = new List<Guid>();

            if (request.NotNullIdsAndRoles())
            {
                users = _unitOfWork.Users.Get(u =>
                request.UserIds.Contains(u.Id) || request.UserRoles.Contains(u.Role)).Select(u => u.Id).ToList();
            }
            else if (request.UserIds != null)
            {
                if (request.UserIds.Any())
                {
                    users = _unitOfWork.Users.Get(u =>
                    request.UserIds.Contains(u.Id)).Select(u => u.Id).ToList();
                }
            }
            else if (request.UserRoles != null)
            {
                if (request.UserRoles.Any())
                {
                    users = _unitOfWork.Users.Get(u =>
                    request.UserRoles.Contains(u.Role)).Select(u => u.Id).ToList();
                }
            }

            if (users.Any())
            {
                var notificationTypeName = StatusExtension<NotificationType>.GetStatus((byte)type);
                result = new List<NotificationViewModel>();
                users.ForEach(u =>
                {
                    result.Add(new NotificationViewModel()
                    {
                        userId = u,
                        Message = request.Message,
                        NotificationType = (byte)type,
                        NotificationTypeName = notificationTypeName,
                        NotificationUrl = url
                    });
                });
            }
            return result;
        }

        private async void SendNotiAsync(List<NotificationViewModel> responses)
        {
            if (responses == null)
                return;
            if (!responses.Any())
                return;
            foreach (var item in responses)
            {
                _logger.LogInformation($"Sending channel notification '{item.Message}' to {item.userId}");
                await _hub.Clients.Group(item.userId.ToString()).SendAsync(item.NotificationUrl, item.NotificationType, item.NotificationTypeName, item.Status, item.StatusName, item.Message);
            }
        }

        private List<string> ConvertErrorObjects(NotificationRequestModel notification)
        {
            var result = new List<string>();
            if (notification != null)
            {
                if (notification.UserIds != null)
                    notification.UserIds.ForEach(id =>
                    {
                        var mess = $"User Id: {id}";
                        mess += !string.IsNullOrEmpty(notification.Message) ? notification.Message : "";
                        result.Add(mess.Trim());
                    });
                if (notification.UserRoles != null)
                    notification.UserRoles.ForEach(role =>
                    {
                        var mess = $"User Role: {role}";
                        mess += !string.IsNullOrEmpty(notification.Message) ? notification.Message : "";
                        result.Add(mess.Trim());
                    });
            }
            return result;
        }
        private List<string> ConvertErrorObjects(List<NotificationRequestModel> notifications)
        {
            var result = new List<string>();
            if (notifications != null)
            {
                notifications.ForEach(notification =>
                {
                    var temp = ConvertErrorObjects(notification);
                    if (temp.Any())
                        result.AddRange(temp);
                });
            }
            return result;
        }
        #endregion
    }
}