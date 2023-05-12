using System.ComponentModel.DataAnnotations;
using Boek.Core.Validations;
using Boek.Infrastructure.Requests.Addresses;

namespace Boek.Infrastructure.Requests.Users.Customers
{
    public class CreateCustomerRequestModel
    {
        [Required]
        public string IdToken { get; set; }
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public bool? Gender { get; set; }
        public AddressRequestModel Address { get; set; }
        [Birthday]
        public DateTime? Dob { get; set; }
        [MaxLength(50)]
        [Phone, Required]
        public string Phone { get; set; }
        public List<int?> GroupIds { get; set; }
    }
}
