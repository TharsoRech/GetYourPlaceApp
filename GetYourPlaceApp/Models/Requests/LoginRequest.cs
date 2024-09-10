namespace GetYourPlaceApp.Models.Requests
{
    public class LoginRequest
    {
        public LoginRequest(string email, string senha)
        {
            Email = email;
            PasswordHash = senha;
        }

        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
    }
}
