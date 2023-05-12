using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Verifications
{
    public class EmailRequestModel
    {
        [EmailAddress]
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}