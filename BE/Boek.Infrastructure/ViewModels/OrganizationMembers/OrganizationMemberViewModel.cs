using Boek.Infrastructure.Attributes;
namespace Boek.Infrastructure.ViewModels.OrganizationMembers
{
    public class OrganizationMemberViewModel
    {
        [Int]
        public int? Id { get; set; }
        [Int]
        public int? OrganizationId { get; set; }
        [String]
        public string EmailDomain { get; set; }
        [Boolean]
        public bool? Status { get; set; }
        [String]
        public string StatusName { get; set; }
    }
}