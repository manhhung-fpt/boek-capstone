using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CampaignGroup
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? GroupId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Group Group { get; set; }
    }
}
