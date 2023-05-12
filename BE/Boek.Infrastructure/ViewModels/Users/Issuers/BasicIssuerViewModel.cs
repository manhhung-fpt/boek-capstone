using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Users.Issuers
{
    public class BasicIssuerViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [String]
        public string Description { get; set; }
    }
}
