using Boek.Service.Interfaces;
using FirebaseAdmin.Auth;

namespace Boek.Service.Services
{
    public class FireBaseService : IFireBaseService
    {
        public async Task<UserRecord> GetUserRecordByIdToken(string idToken)
        {
            try
            {
                var auth = FirebaseAuth.DefaultInstance;
                FirebaseToken token = await auth.VerifyIdTokenAsync(idToken);
                UserRecord record = await auth.GetUserAsync(token.Uid);
                return record;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<UserRecord> GetUserRecordByEmail(string email)
        {
            try
            {
                var auth = FirebaseAuth.DefaultInstance;
                UserRecord record = await auth.GetUserByEmailAsync(email);
                return record;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public async Task<string> GenerateVerificationLinkByEmail(string email)
        {
            try
            {
                var auth = FirebaseAuth.DefaultInstance;
                var link = await auth.GenerateEmailVerificationLinkAsync(email);
                return link;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
