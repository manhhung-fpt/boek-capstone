namespace Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts
{
    public class BasicUpdateBookSeriesProductRequestModel
    {
        public Guid Id { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
        public byte? Status { get; set; }
    }
}