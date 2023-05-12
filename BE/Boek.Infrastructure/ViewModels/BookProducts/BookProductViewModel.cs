using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Books;
using Boek.Infrastructure.ViewModels.Campaigns;
using Boek.Infrastructure.ViewModels.Users.Issuers;
using Boek.Infrastructure.ViewModels.BookProductItems;
using Boek.Infrastructure.ViewModels.Genres;
using System.Text.Json.Serialization;

namespace Boek.Infrastructure.ViewModels.BookProducts
{
    public class BookProductViewModel
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
        public int? SaleQuantity { get; set; }
        [Int]
        public int? Discount { get; set; }
        [Decimal]
        public decimal? SalePrice { get; set; }
        [Int]
        public int? Commission { get; set; }
        [Byte]
        public byte? Type { get; set; }
        [String]
        public string TypeName { get; set; }
        [Byte]
        public byte? Format { get; set; }
        [String]
        public string FormatName { get; set; }
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
        [Skip]
        public bool? FullPdfAndAudio { get; set; } = false;
        [Skip]
        public bool? OnlyPdf { get; set; } = false;
        [Skip]
        public bool? OnlyAudio { get; set; } = false;
        [Sort, JsonIgnore]
        public string Sort { get; set; }

        public BasicCampaignViewModel Campaign { get; set; }
        public BookProductDetailViewModel Book { get; set; }
        public GenreViewModel Genre { get; set; }
        public IssuerViewModel Issuer { get; set; }
        public List<BookProductItemViewModel> BookProductItems { get; set; }

    }
}