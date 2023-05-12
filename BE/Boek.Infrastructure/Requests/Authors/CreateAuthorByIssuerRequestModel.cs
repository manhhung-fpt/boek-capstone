using System.ComponentModel.DataAnnotations;

namespace Boek.Infrastructure.Requests.Authors
{
    public class CreateAuthorByIssuerRequestModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
