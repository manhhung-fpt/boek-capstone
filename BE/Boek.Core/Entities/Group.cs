using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Group
    {
        public Group()
        {
            CampaignGroups = new HashSet<CampaignGroup>();
            CustomerGroups = new HashSet<CustomerGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<CampaignGroup> CampaignGroups { get; set; }
        public virtual ICollection<CustomerGroup> CustomerGroups { get; set; }
    }
}
