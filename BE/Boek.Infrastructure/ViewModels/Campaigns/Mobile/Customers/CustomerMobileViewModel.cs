using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Groups;
using Boek.Infrastructure.ViewModels.Organizations;
using Boek.Infrastructure.ViewModels.Users;

namespace Boek.Infrastructure.ViewModels.Campaigns.Mobile.Customers
{
    public class CustomerMobileViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [Int]
        public int? LevelId { get; set; }
        [DateRange]
        public DateTime? Dob { get; set; }
        [Boolean]
        public bool? Gender { get; set; }
        [Int]
        public int? Point { get; set; }
        public UserViewModel User { get; set; }
        public List<BasicGroupViewModel> Groups { get; set; }
        public List<BasicOrganizationViewModel> Organizations { get; set; }
    }
}