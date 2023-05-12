using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Campaign
    {
        public Campaign()
        {
            BookProducts = new HashSet<BookProduct>();
            CampaignCommissions = new HashSet<CampaignCommission>();
            CampaignGroups = new HashSet<CampaignGroup>();
            CampaignLevels = new HashSet<CampaignLevel>();
            CampaignOrganizations = new HashSet<CampaignOrganization>();
            CampaignStaffs = new HashSet<CampaignStaff>();
            Orders = new HashSet<Order>();
            Participants = new HashSet<Participant>();
        }

        public int Id { get; set; }
        public Guid? Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte? Format { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsRecurring { get; set; }
        public byte? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<BookProduct> BookProducts { get; set; }
        public virtual ICollection<CampaignCommission> CampaignCommissions { get; set; }
        public virtual ICollection<CampaignGroup> CampaignGroups { get; set; }
        public virtual ICollection<CampaignLevel> CampaignLevels { get; set; }
        public virtual ICollection<CampaignOrganization> CampaignOrganizations { get; set; }
        public virtual ICollection<CampaignStaff> CampaignStaffs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
