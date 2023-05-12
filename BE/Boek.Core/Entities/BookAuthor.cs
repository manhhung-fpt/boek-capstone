using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class BookAuthor
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }
        public int? DisplayIndex { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
