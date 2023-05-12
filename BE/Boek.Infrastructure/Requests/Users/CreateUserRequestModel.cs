using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Users
{
    public class CreateUserRequestModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [Required]
        public byte? Role { get; set; }
    }
}
