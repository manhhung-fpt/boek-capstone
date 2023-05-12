using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Genres
{
    public class CreateGenreRequestModel
    {
        public int? ParentId { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
