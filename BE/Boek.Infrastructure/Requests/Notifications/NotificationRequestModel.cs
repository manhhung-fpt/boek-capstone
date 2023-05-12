namespace Boek.Infrastructure.Requests.Notifications
{
    public class NotificationRequestModel
    {
        public List<Guid?> UserIds { get; set; }
        public List<byte> UserRoles { get; set; }
        public byte? Status { get; set; }
        public string StatusName { get; set; }
        public string Message { get; set; }

        public bool NotNullIdsAndRoles()
        {
            if (this.UserIds == null || this.UserRoles == null)
                return false;
            return this.UserIds.Any() && this.UserRoles.Any();
        }

        public bool IsEmptyIdsAndRoles()
        {
            if (this.UserIds == null && this.UserRoles == null)
                return true;
            return false;
        }
    }
}