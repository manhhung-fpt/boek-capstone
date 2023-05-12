using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Publisher
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
