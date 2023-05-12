using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class User
    {
        public User()
        {
            CampaignStaffs = new HashSet<CampaignStaff>();
        }

        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public byte Role { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Issuer Issuer { get; set; }
        public virtual ICollection<CampaignStaff> CampaignStaffs { get; set; }
    }
}
