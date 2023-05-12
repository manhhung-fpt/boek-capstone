using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
