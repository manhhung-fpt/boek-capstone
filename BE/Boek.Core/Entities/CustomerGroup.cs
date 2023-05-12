using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class CustomerGroup
    {
        public int Id { get; set; }
        public Guid? CustomerId { get; set; }
        public int? GroupId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Group Group { get; set; }
    }
}
