using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.OrderDetails;

namespace Boek.Infrastructure.ViewModels.BookProducts
{
    public class BookProductOrderDetailsViewModel
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
        public int Total { get; set; } = 0;
        public List<BasicOrderDetailsViewModel> OrderDetails { get; set; }

        public void UpdateTotal(bool? IsDescendingData = null, bool? IsDescendingTimeLine = null)
        {
            if (OrderDetails != null)
            {
                if (OrderDetails.Any())
                {
                    Total = OrderDetails.Sum(o => o.Quantity);
                    var result = !IsDescendingTimeLine.HasValue || false.Equals(IsDescendingTimeLine);
                    if (IsDescendingData.HasValue && result)
                    {
                        if ((bool)IsDescendingData)
                            OrderDetails = OrderDetails.OrderByDescending(item => item.Quantity).ToList();
                        else
                            OrderDetails = OrderDetails.OrderBy(item => item.Quantity).ToList();
                    }
                }
            }
        }

    }
}