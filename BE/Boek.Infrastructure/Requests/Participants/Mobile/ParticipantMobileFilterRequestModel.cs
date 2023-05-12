using Boek.Infrastructure.Attributes;
using Boek.Core.Enums;

namespace Boek.Infrastructure.Requests.Participants.Mobile
{
    public class ParticipantMobileFilterRequestModel
    {
        [Range, GuidRange, SkipRange]
        public List<Guid?> IssuerIds { get; set; }
    }
}