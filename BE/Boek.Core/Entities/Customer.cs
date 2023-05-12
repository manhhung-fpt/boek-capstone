using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerGroups = new HashSet<CustomerGroup>();
            CustomerOrganizations = new HashSet<CustomerOrganization>();
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public int? LevelId { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
        public int? Point { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<CustomerGroup> CustomerGroups { get; set; }
        public virtual ICollection<CustomerOrganization> CustomerOrganizations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
