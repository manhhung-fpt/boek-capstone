using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.OrganizationMembers
{
    public class CreateOrganizationMemberRequestModel
    {
        [MaxLength(255)]
        public string EmailDomain { get; set; }
    }
}