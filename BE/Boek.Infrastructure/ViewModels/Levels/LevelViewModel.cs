using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Users.Customers;

namespace Boek.Infrastructure.ViewModels.Levels
{
    public class LevelViewModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [Int]
        public int? ConditionalPoint { get; set; }

        [Boolean]
        public bool? Status { get; set; }

        [String]
        public string StatusName { get; set; }

        public List<CustomerUserViewModel> Customers { get; set; }
    }
}
