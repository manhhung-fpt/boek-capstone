using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Requests.Books.BookItems;

namespace Boek.Infrastructure.Requests.Books.BookSeries
{
    public class CreateBookSeriesRequestModel
    {
        public string Code { get; set; }
        public int? GenreId { get; set; }
        [MaxLength(50)]
        public string Isbn10 { get; set; }
        [MaxLength(50)]
        public string Isbn13 { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal CoverPrice { get; set; }
        public string Description { get; set; }
        public int ReleasedYear { get; set; }
        public List<CreateBookItemRequestModel> createBookItems { get; set; }
    }
}
