using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CampaignOrganization
    {
        public CampaignOrganization()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public int? OrganizationId { get; set; }
        public int? CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
