using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.Requests.Levels
{
    public class LevelRequestModel
    {
        [Int]
        public int? Id { get; set; }

        [String]
        public string Name { get; set; }

        [Int]
        public int? ConditionalPoint { get; set; }

        [Boolean]
        public bool? Status { get; set; }
    }
}
