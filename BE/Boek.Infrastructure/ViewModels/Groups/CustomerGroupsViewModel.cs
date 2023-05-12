using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Groups
{
    public class CustomerGroupsViewModel
    {
        [Int]
        public int? Total { get; set; }
        public BasicGroupViewModel Group { get; set; }
    }
}