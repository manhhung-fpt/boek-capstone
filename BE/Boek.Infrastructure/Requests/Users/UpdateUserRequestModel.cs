using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Boek.Infrastructure.Requests.Addresses;

namespace Boek.Infrastructure.Requests.Users
{
    public class UpdateUserRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(50)]
        [Phone]
        public string Phone { get; set; }
        public AddressRequestModel AddressRequest { get; set; }
        [JsonIgnore]
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public byte? Role { get; set; }
        public bool? Status { get; set; }
    }
}
