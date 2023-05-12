namespace Boek.Infrastructure.Requests.BookProducts
{
    public class UpdateBookProductRequestModel
    {
        public Guid Id { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
        public bool? WithPdf { get; set; }
        public int? DisplayPdfIndex { get; set; }
        public bool? WithAudio { get; set; }
        public int? DisplayAudioIndex { get; set; }
        public byte? Status { get; set; }
    }
}