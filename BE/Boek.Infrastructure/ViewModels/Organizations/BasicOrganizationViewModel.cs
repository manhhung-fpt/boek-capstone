using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Organizations
{
    public class BasicOrganizationViewModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Address { get; set; }

        [String]
        public string Phone { get; set; }

        [String, Url]
        public string ImageUrl { get; set; }
    }
}
