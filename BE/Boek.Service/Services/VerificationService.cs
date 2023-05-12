using Boek.Core.Constants;
using Boek.Infrastructure.Requests.Verifications;
using Boek.Service.Commons;
using Boek.Service.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using System.Net;

namespace Boek.Service.Services
{
    public class VerificationService : IVerificationService
    {
        #region Fields and constructor
        private readonly IConfiguration _configuration;
        private readonly IFireBaseService _fireBaseService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerificationService(IConfiguration configuration,
        IFireBaseService fireBaseService,
        IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _fireBaseService = fireBaseService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Email
        public void SendEmail(EmailRequestModel request, bool ShowErrorMessage = true)
        {
            try
            {
                var boekEmail = _configuration.GetSection(MessageConstants.BOEK_EMAIL);
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(boekEmail[MessageConstants.BOEK_EMAIL_USER_NAME]));
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = request.Body.Trim() };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(boekEmail[MessageConstants.BOEK_EMAIL_HOST], 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate(boekEmail[MessageConstants.BOEK_EMAIL_USER_NAME], boekEmail[MessageConstants.BOEK_EMAIL_PASSWORD]);
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ShowErrorMessage)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.InternalServerError, new string[] { ex.Message });
            }
        }
        #endregion

        #region OTP
        public void SendOTPEmail(EmailRequestModel email) => OtpEmail(email);

        public void ResendOTPEmail(EmailRequestModel email) => OtpEmail(email, false);

        public string ValidateOTPEmail(OtpRequestModel OtpRequest)
        {
            if (OtpRequest.IsNullEmail())
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    MessageConstants.MESSAGE_REQUIRED,
                    ErrorMessageConstants.USER_EMAIL
                });
            string newToken = null;
            var otp = GetOtpSession();
            if (otp.HasValue)
            {
                if (!otp.Equals(OtpRequest.Otp))
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    MessageConstants.MESSAGE_NOT_DUPLICATED_INFO,
                    ErrorMessageConstants.VERIFICATION_OTP
                });
                else
                {
                    _httpContextAccessor.HttpContext.Session.Clear();
                    newToken = AccessTokenManager.GenerateGuestJwtToken(OtpRequest.Name, OtpRequest.Email, _configuration);
                }
            }
            else
                BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.NotFound, new string[]
                {
                    ErrorMessageConstants.NOT_FOUND,
                    ErrorMessageConstants.VERIFICATION_OTP
                });
            return newToken;
        }
        #endregion

        #region Utils
        private int GenerateOTP()
        {
            var _random = new Random();
            return _random.Next(100000, 999999);
        }

        private int? GetOtpSession()
        => ((int?)_httpContextAccessor.HttpContext.Session.GetInt32(MessageConstants.SESSION_OTP_VERIFICATION));

        private void OtpEmail(EmailRequestModel email, bool firstTime = true)
        {
            var OtpSession = GetOtpSession();
            if (firstTime)
            {
                if (OtpSession.HasValue)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    ErrorMessageConstants.VERIFICATION_INVALID_OTP
                });
            }
            else
            {
                if (!OtpSession.HasValue)
                    BoekErrorMessage.ShowErrorMessage((int)HttpStatusCode.BadRequest, new string[]
                {
                    ErrorMessageConstants.VERIFICATION_INVALID_OTP
                });
            }
            //Generate OTP
            var otp = GenerateOTP();
            _httpContextAccessor.HttpContext.Session.SetInt32(MessageConstants.SESSION_OTP_VERIFICATION, otp);
            email.Body = email.Body.Trim();
            email.Body += " " + otp;
            SendEmail(email);
        }
        #endregion
    }
}