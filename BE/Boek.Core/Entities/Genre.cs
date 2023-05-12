using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            BookProducts = new HashSet<BookProduct>();
            Books = new HashSet<Book>();
            CampaignCommissions = new HashSet<CampaignCommission>();
            InverseParent = new HashSet<Genre>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int? DisplayIndex { get; set; }
        public bool? Status { get; set; }

        public virtual Genre Parent { get; set; }
        public virtual ICollection<BookProduct> BookProducts { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<CampaignCommission> CampaignCommissions { get; set; }
        public virtual ICollection<Genre> InverseParent { get; set; }
    }
}
