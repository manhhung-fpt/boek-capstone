namespace Boek.Infrastructure.Requests.BookProductItems
{
    public class UpdateBookProductItemRequestModel
    {
        public int? BookId { get; set; }
        public byte? Format { get; set; }
        public int? DisplayIndex { get; set; }
        public bool? WithPdf { get; set; }
        public int? DisplayPdfIndex { get; set; }
        public bool? WithAudio { get; set; }
        public int? DisplayAudioIndex { get; set; }
    }
}