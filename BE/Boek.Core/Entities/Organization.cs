using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Organization
    {
        public Organization()
        {
            CampaignOrganizations = new HashSet<CampaignOrganization>();
            CustomerOrganizations = new HashSet<CustomerOrganization>();
            OrganizationMembers = new HashSet<OrganizationMember>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<CampaignOrganization> CampaignOrganizations { get; set; }
        public virtual ICollection<CustomerOrganization> CustomerOrganizations { get; set; }
        public virtual ICollection<OrganizationMember> OrganizationMembers { get; set; }
    }
}
