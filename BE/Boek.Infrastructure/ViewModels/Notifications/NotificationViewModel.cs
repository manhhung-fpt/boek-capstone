namespace Boek.Infrastructure.ViewModels.Notifications
{
    public class NotificationViewModel
    {
        public Guid? userId { get; set; }
        public string Message { get; set; }
        public byte NotificationType { get; set; }
        public string NotificationTypeName { get; set; }
        public byte? Status { get; set; }
        public string StatusName { get; set; }
        public string NotificationUrl { get; set; }
    }
}