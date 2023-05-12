using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Users.Issuers
{
    public class IssuerViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [String]
        public string Description { get; set; }
        public UserViewModel User { get; set; }
    }
}
