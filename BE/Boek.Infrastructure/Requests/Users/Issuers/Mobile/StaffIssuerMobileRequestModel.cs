using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Users.Issuers.Mobile
{
    public class StaffIssuerMobileRequestModel
    {
        [Range, SkipRange, Guid]
        public Guid? Id { get; set; }
    }
}