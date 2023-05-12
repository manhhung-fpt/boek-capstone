using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class BookProduct
    {
        public BookProduct()
        {
            BookProductItems = new HashSet<BookProductItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public int? BookId { get; set; }
        public int? GenreId { get; set; }
        public int? CampaignId { get; set; }
        public Guid? IssuerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public decimal SalePrice { get; set; }
        public int Commission { get; set; }
        public byte? Type { get; set; }
        public bool? WithPdf { get; set; }
        public decimal? PdfExtraPrice { get; set; }
        public int? DisplayPdfIndex { get; set; }
        public bool? WithAudio { get; set; }
        public decimal? AudioExtraPrice { get; set; }
        public int? DisplayAudioIndex { get; set; }
        public byte? Status { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual Campaign Campaign { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Issuer Issuer { get; set; }
        public virtual ICollection<BookProductItem> BookProductItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
