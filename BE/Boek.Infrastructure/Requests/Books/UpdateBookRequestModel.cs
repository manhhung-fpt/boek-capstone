using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;

namespace Boek.Infrastructure.Requests.Books
{
    public class UpdateBookRequestModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int? GenreId { get; set; }
        public int? PublisherId { get; set; }
        [MaxLength(50)]
        public string Isbn10 { get; set; }
        [MaxLength(50)]
        public string Isbn13 { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Translator { get; set; }
        public string ImageUrl { get; set; }
        public decimal CoverPrice { get; set; }
        public string Description { get; set; }
        [MaxLength(255)]
        public string Language { get; set; }
        [MaxLength(255)]
        public string Size { get; set; }
        public int ReleasedYear { get; set; }
        public int Page { get; set; }
        public decimal? PdfExtraPrice { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.BOOK_PDF_LINK}")]
        public string PdfTrialUrl { get; set; }
        public decimal? AudioExtraPrice { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.BOOK_AUDIO_LINK}")]
        public string AudioTrialUrl { get; set; }
        public byte Status { get; set; }
        public List<int?> Authors { get; set; }
    }
}
