using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Level
    {
        public Level()
        {
            CampaignLevels = new HashSet<CampaignLevel>();
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ConditionalPoint { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<CampaignLevel> CampaignLevels { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
