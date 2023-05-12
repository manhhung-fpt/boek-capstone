using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Books
{
    public class BasicBookViewModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Code { get; set; }
        [Int]
        public int? GenreId { get; set; }
        [Int]
        public int? PublisherId { get; set; }
        [Guid]
        public Guid? IssuerId { get; set; }
        [String]
        public string Isbn10 { get; set; }
        [String]
        public string Isbn13 { get; set; }
        [String]
        public string Name { get; set; }
        [String]
        public string Translator { get; set; }
        [String]
        public string ImageUrl { get; set; }
        [Decimal]
        public decimal? CoverPrice { get; set; }
        [String]
        public string Description { get; set; }
        [String]
        public string Language { get; set; }
        [String]
        public string Size { get; set; }
        [Int]
        public int? ReleasedYear { get; set; }
        [Int]
        public int? Page { get; set; }
        [Boolean]
        public bool? IsSeries { get; set; }
        [Decimal]
        public decimal? PdfExtraPrice { get; set; }
        [String]
        public string PdfTrialUrl { get; set; }
        [Decimal]
        public decimal? AudioExtraPrice { get; set; }
        [String]
        public string AudioTrialUrl { get; set; }
        [Byte]
        public byte? Status { get; set; }
        [String]
        public string StatusName { get; set; }
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
        [Skip]
        public bool? FullPdfAndAudio { get; set; } = false;
        [Skip]
        public bool? OnlyPdf { get; set; } = false;
        [Skip]
        public bool? OnlyAudio { get; set; } = false;
    }
}
