using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookItemBooks = new HashSet<BookItem>();
            BookItemParentBooks = new HashSet<BookItem>();
            BookProductItems = new HashSet<BookProductItem>();
            BookProducts = new HashSet<BookProduct>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? GenreId { get; set; }
        public int? PublisherId { get; set; }
        public Guid? IssuerId { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        public string Name { get; set; }
        public string Translator { get; set; }
        public string ImageUrl { get; set; }
        public decimal CoverPrice { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Size { get; set; }
        public int ReleasedYear { get; set; }
        public int? Page { get; set; }
        public bool? IsSeries { get; set; }
        public decimal? PdfExtraPrice { get; set; }
        public string PdfTrialUrl { get; set; }
        public decimal? AudioExtraPrice { get; set; }
        public string AudioTrialUrl { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Issuer Issuer { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookItem> BookItemBooks { get; set; }
        public virtual ICollection<BookItem> BookItemParentBooks { get; set; }
        public virtual ICollection<BookProductItem> BookProductItems { get; set; }
        public virtual ICollection<BookProduct> BookProducts { get; set; }
    }
}
