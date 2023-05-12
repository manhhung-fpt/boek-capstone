using System.ComponentModel.DataAnnotations;
using Boek.Core.Constants;
using Boek.Infrastructure.Requests.OrganizationMembers;

namespace Boek.Infrastructure.Requests.Organizations
{
    public class CreateOrganizationRequestModel
    {
        [MaxLength(255)]
        [Required(ErrorMessage = $"{MessageConstants.MESSAGE_REQUIRED} {ErrorMessageConstants.ORGANIZATION_NAME}")]
        public string Name { get; set; }

        public string Address { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [Url(ErrorMessage = $"{MessageConstants.MESSAGE_INVALID} {ErrorMessageConstants.ORGANIZATION_IMAGE_URL}")]
        public string ImageUrl { get; set; }

        public List<CreateOrganizationMemberRequestModel> OrganizationMembers { get; set; }
    }
}