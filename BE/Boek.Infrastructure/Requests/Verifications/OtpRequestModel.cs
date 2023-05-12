using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Verifications
{
    public class OtpRequestModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Otp { get; set; }

        public bool IsNullEmail() => string.IsNullOrEmpty(Email);
    }
}