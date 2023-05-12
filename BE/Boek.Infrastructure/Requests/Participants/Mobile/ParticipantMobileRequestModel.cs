using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Participants.Mobile
{
    public class ParticipantMobileRequestModel
    {
        [Range, GuidRange, SkipRange]
        public List<Guid?> IssuerIds { get; set; }
    }
}