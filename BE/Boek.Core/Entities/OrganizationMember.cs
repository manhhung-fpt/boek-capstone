using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class OrganizationMember
    {
        public int Id { get; set; }
        public int? OrganizationId { get; set; }
        public string EmailDomain { get; set; }
        public bool? Status { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
