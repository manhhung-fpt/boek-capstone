using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Schedule
    {
        public int Id { get; set; }
        public int? CampaignOrganizationId { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? Status { get; set; }

        public virtual CampaignOrganization CampaignOrganization { get; set; }
    }
}
