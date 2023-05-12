namespace Boek.Infrastructure.Requests.Users.Issuers
{
    public class UpdateIssuerRequestModel
    {
        public string Description { get; set; }
        public UpdateUserRequestModel User { get; set; }
    }
}
