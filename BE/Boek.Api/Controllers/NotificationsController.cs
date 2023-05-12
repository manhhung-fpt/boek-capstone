using Boek.Infrastructure.Requests.Notifications;
using Boek.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Boek.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        #region Field(s) and constructor
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        #endregion

        #region Book Product
        [HttpPost("book/product")]
        public ActionResult SendBookProductNotification([FromBody] NotificationRequestModel notification)
        {
            _notificationService.PushCheckingBookProductNotification(notification);
            return Ok();
        }
        #endregion

        #region Participant
        [HttpPost("participant")]
        public ActionResult SendParticipantNotification([FromBody] NotificationRequestModel notification)
        {
            _notificationService.PushParticipantNotification(notification);
            return Ok();
        }
        #endregion

        #region Order
        [HttpPost("order")]
        public ActionResult SendOrderNotification([FromBody] NotificationRequestModel notification)
        {
            _notificationService.PushCancelledOrderByCustomerNotification(notification);
            return Ok();
        }
        #endregion
    }
}