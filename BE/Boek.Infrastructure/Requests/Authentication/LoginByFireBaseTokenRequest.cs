using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Authentication
{
    public class LoginByFireBaseTokenRequest
    {
        [Required]
        public string IdToken { get; set; }

        public string FcmToken { get; set; } = "";
    }
}
