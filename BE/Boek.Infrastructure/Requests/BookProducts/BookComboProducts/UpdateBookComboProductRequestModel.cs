using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.BookProductItems;

namespace Boek.Infrastructure.Requests.BookProducts.BookComboProducts
{
    public class UpdateBookComboProductRequestModel
    {
        public Guid Id { get; set; }
        public int? GenreId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public byte? Format { get; set; }
        public int SaleQuantity { get; set; }
        public decimal SalePrice { get; set; }
        public int Commission { get; set; }
        public byte? Status { get; set; }
        public List<UpdateBookProductItemRequestModel> BookProductItems { get; set; }
    }
}