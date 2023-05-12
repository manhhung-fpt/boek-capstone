using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Genres
{
    public class UpdateGenreRequestModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public int? DisplayIndex { get; set; }
        public bool? Status { get; set; }
    }
}
