using Boek.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Users
{
    public class UserRequestModel
    {
        [Guid]
        public Guid? Id { get; set; }
        [String]
        public string Code { get; set; }
        [String]
        public string Name { get; set; }
        [String, EmailAddress]
        public string Email { get; set; }
        [String]
        public string Address { get; set; }
        [String, Phone]
        public string Phone { get; set; }
        [Byte]
        public byte? Role { get; set; }
        [Boolean]
        public bool? Status { get; set; }
        [String, Url]
        public string ImageUrl { get; set; }
        [Skip]
        public bool? WithAddressDetail { get; set; } = false;
    }
}
