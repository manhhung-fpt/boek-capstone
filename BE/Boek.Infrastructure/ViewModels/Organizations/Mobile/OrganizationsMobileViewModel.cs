using System.ComponentModel.DataAnnotations;
using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Schedules;

namespace Boek.Infrastructure.ViewModels.Organizations.Mobile
{
    public class OrganizationsMobileViewModel
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

        public List<SchedulesViewModel> Schedules { get; set; }
    }
}