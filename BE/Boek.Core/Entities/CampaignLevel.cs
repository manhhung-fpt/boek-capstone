using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CampaignLevel
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? LevelId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Level Level { get; set; }
    }
}
