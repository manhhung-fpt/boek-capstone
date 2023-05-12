using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.BookProductItems
{
    public class BookProductItemRequestModel
    {
        [Int]
        public int? Id { get; set; }
        [Guid]
        public Guid? ParentBookProductId { get; set; }
        [Int]
        public int? BookId { get; set; }
        [Byte]
        public byte? Format { get; set; }
        [Int]
        public int? DisplayIndex { get; set; }
        [Boolean]
        public bool? WithPdf { get; set; }
        [Decimal]
        public decimal? PdfExtraPrice { get; set; }
        [Int]
        public int? DisplayPdfIndex { get; set; }
        [Boolean]
        public bool? WithAudio { get; set; }
        [Decimal]
        public decimal? AudioExtraPrice { get; set; }
        [Int]
        public int? DisplayAudioIndex { get; set; }
    }
}