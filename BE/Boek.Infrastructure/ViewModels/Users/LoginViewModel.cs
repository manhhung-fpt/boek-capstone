namespace Boek.Infrastructure.ViewModels.Users
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            IsFirstLogin = false;
        }
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public bool IsFirstLogin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public byte? Role { get; set; }
        public DateTime? Dob { get; set; }
        public string ImageUrl { get; set; }
    }
}
