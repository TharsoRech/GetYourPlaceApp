using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Requests;

namespace GetYourPlaceApp.Repository.Login
{
    public interface ILoginRepository
    {
        Task<GYPUser> LoginAsync(LoginRequest loginRequest);
    }
}
