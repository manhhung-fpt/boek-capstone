using Boek.Infrastructure.Attributes;

namespace Boek.Infrastructure.ViewModels.Users.Customers
{
    public class CustomerUserViewModel
    {
        /*[Guid]
        public Guid? Id { get; set; }*/
        [Int]
        public int? LevelId { get; set; }
        [DateRange]
        public DateTime? Dob { get; set; }
        [Boolean]
        public bool? Gender { get; set; }
        [Int]
        public int? Point { get; set; }
        public UserViewModel User { get; set; }
    }
}
