using Flunt.Validations;
using GetYourPlaceApp.Models.Requests;

namespace GetYourPlaceApp.Contracts
{
    public class LoginContract: Contract<LoginRequest>
    {
        public LoginContract(LoginRequest request)
        {
            Requires()
                .IsNotNullOrEmpty(request.Email, "Email", "Email cannot be empty")
                .IsNotNullOrEmpty(request.PasswordHash, "Password", "Password cannot be empty");
        }
    }
}
