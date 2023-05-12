using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.OrganizationMembers
{
    public class UpdateOrganizationMemberRequestModel
    {
        [MaxLength(255)]
        public string EmailDomain { get; set; }
        public bool? Status { get; set; }
    }
}