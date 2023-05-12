using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.BookProducts
{
    public class BasicBookProductViewModel
    {
        [Guid]
        public Guid Id { get; set; }
        [Int]
        public int? BookId { get; set; }
        [Int]
        public int? GenreId { get; set; }
        [Int]
        public int? CampaignId { get; set; }
        [Guid]
        public Guid? IssuerId { get; set; }
        [String]
        public string Title { get; set; }
        [String]
        public string Description { get; set; }
        [String]
        public string ImageUrl { get; set; }
        [Int]
        public int SaleQuantity { get; set; }
        [Int]
        public int? Discount { get; set; }
        [Decimal]
        public decimal SalePrice { get; set; }
        [Int]
        public int? Commission { get; set; }
        [Byte]
        public byte? Type { get; set; }
        [Byte]
        public byte? Format { get; set; }
        [Boolean]
        public bool? WithPdf { get; set; }
        [Decimal]
        public decimal? PdfExtraPrice { get; set; }
        [Int]
        public int? DisplayPdfIndex { get; set; }
        [Boolean]
        public bool? WithAudio { get; set; }
        [Int]
        public int? DisplayAudioIndex { get; set; }
        [Decimal]
        public decimal? AudioExtraPrice { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [String]
        public string StatusName { get; set; }
        [String]
        public string Note { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
    }
}