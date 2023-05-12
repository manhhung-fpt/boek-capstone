using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CampaignStaff
    {
        public CampaignStaff()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public Guid? StaffId { get; set; }
        public byte? Status { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual User Staff { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
