using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Participant
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public Guid? IssuerId { get; set; }
        public byte Status { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual Issuer Issuer { get; set; }
    }
}
