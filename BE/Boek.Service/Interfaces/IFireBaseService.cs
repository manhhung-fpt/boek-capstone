using FirebaseAdmin.Auth;

namespace Boek.Service.Interfaces
{
    public interface IFireBaseService
    {
        Task<UserRecord> GetUserRecordByIdToken(string idToken);
        Task<UserRecord> GetUserRecordByEmail(string email);
        Task<string> GenerateVerificationLinkByEmail(string email);
    }
}
