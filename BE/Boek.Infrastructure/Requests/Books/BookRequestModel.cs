using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Books
{
    public class BookRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [String]
        public string Code { get; set; }
        [Range, IntRange]
        public List<int?> GenreIds { get; set; }
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
        public string BookSize { get; set; }
        [Int]
        public int? ReleasedYear { get; set; }
        public int? BookPage { get; set; }
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
        [DateRange]
        public DateTime? CreatedDate { get; set; }
        [DateRange]
        public DateTime? UpdatedDate { get; set; }
        [Sort]
        public string Sort { get; set; }
    }
}
