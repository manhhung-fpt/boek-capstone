using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Groups
{
    public class BasicGroupViewModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string Description { get; set; }

        [Boolean]
        public bool? Status { get; set; }
        [String]
        public string StatusName { get; set; }
    }
}
