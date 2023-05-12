using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CampaignCommission
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? GenreId { get; set; }
        public int? MinimalCommission { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
