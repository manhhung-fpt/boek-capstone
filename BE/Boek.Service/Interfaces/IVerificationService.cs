using Boek.Infrastructure.Requests.Verifications;

namespace Boek.Service.Interfaces
{
    public interface IVerificationService
    {
        public void SendEmail(EmailRequestModel email, bool ShowErrorMessage = true);
        public void SendOTPEmail(EmailRequestModel email);
        public void ResendOTPEmail(EmailRequestModel email);
        public string ValidateOTPEmail(OtpRequestModel OtpRequest);
    }
}