using Boek.Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Publishers
{
    public class UpdatePublisherRequestModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.PUBLISHER_EMAIL}")]
        public string Email { get; set; }
        public string Address { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.PUBLISHER_URL}")]
        public string ImageUrl { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
    }
}
