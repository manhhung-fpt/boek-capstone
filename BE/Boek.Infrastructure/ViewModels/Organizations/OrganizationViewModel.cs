using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.OrganizationMembers;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.Organizations
{
    public class OrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Address { get; set; }

        [String]
        public string Phone { get; set; }

        [String, Url]
        public string ImageUrl { get; set; }

        public List<CustomerUserViewModel> Customers { get; set; }

        public List<BasicCampaignViewModel> Campaigns { get; set; }

        public List<OrganizationMemberViewModel> Members { get; set; }
    }
}
