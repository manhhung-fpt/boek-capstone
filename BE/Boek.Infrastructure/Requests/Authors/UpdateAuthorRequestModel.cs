using Boek.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Authors
{
    public class UpdateAuthorRequestModel
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.AUTHOR_URL}")]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
