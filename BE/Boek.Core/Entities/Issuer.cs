using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Issuer
    {
        public Issuer()
        {
            BookProducts = new HashSet<BookProduct>();
            Books = new HashSet<Book>();
            Participants = new HashSet<Participant>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual ICollection<BookProduct> BookProducts { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
