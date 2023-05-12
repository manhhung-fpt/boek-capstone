using Boek.Infrastructure.Requests.BookProductItems;

namespace Boek.Infrastructure.Requests.BookProducts.BookSeriesProducts
{
    public class UpdateBookSeriesProductRequestModel
    {
        public Guid Id { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public int? Discount { get; set; }
        public int Commission { get; set; }
        public byte? Status { get; set; }
        public List<UpdateBookProductItemRequestModel> BookProductItems { get; set; }
    }
}