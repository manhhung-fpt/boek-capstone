namespace Boek.Infrastructure.Requests.BookProducts
{
    public class CreateBookProductRequestModel
    {
        public int? BookId { get; set; }
        public int? CampaignId { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
        public bool? WithPdf { get; set; }
        public int? DisplayPdfIndex { get; set; }
        public bool? WithAudio { get; set; }
        public int? DisplayAudioIndex { get; set; }
    }
}