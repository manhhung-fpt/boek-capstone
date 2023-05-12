namespace Boek.Infrastructure.Requests.Verifications
{
    public class RedisOtpModel
    {
        public Guid Id { get; set; }
        public int? Otp { get; set; }
        public DateTimeOffset Expire { get; set; }
    }
}