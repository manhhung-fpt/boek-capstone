using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CustomerOrganization
    {
        public int Id { get; set; }
        public Guid? CustomerId { get; set; }
        public int? OrganizationId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
