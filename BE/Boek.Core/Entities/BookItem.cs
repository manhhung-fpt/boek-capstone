using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class BookItem
    {
        public int Id { get; set; }
        public int? ParentBookId { get; set; }
        public int? BookId { get; set; }
        public int? DisplayIndex { get; set; }

        public virtual Book Book { get; set; }
        public virtual Book ParentBook { get; set; }
    }
}
