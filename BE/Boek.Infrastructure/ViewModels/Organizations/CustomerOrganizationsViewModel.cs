using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Organizations
{
    public class CustomerOrganizationsViewModel
    {
        [Int]
        public int? Total { get; set; }
        public BasicOrganizationViewModel Organization { get; set; }
    }
}