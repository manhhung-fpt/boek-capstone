using System.ComponentModel.DataAnnotations;
using Boek.Core.Validations;

namespace Boek.Infrastructure.Requests.Users.Customers
{
    public class UpdateCustomerRequestModel
    {
        [Birthday]
        public DateTime? Dob { get; set; }
        [Required]
        public bool? Gender { get; set; }
        public UpdateUserRequestModel User { get; set; }
    }
}
