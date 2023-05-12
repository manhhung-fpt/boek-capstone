using Boek.Infrastructure.Attributes;
using Boek.Infrastructure.ViewModels.Levels;

namespace Boek.Infrastructure.ViewModels.Users.Customers
{
    public class CustomerLevelViewModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [Int]
        public int? LevelId { get; set; }
        [DateRange]
        public DateTime? Dob { get; set; }
        [Boolean]
        public bool? Gender { get; set; }
        [Int]
        public int? Point { get; set; }
        public BasicLevelViewModel Level { get; set; }
    }
}
