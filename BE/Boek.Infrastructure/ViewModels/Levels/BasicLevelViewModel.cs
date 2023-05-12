using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Levels
{
    public class BasicLevelViewModel
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
    }
}
