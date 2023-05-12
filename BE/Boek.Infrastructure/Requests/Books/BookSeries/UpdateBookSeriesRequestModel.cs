using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.Requests.Books.BookItems;

namespace Boek.Infrastructure.Requests.Books.BookSeries
{
    public class UpdateBookSeriesRequestModel
    {
        public int Id { get; set; }
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
        public byte Status { get; set; }
        public List<UpdateBookItemRequestModel> updateBookItems { get; set; }
    }
}
