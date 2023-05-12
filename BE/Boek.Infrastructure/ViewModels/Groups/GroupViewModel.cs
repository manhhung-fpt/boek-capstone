using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.Groups
{
    public class GroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [Boolean]
        public bool? Status { get; set; }
        [String]
        public string StatusName { get; set; }

        public List<CustomerUserViewModel> Customers { get; set; }

        public List<BasicCampaignViewModel> Campaigns { get; set; }
    }
}
