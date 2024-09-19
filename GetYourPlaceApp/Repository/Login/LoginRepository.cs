using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Requests;
using RestSharp;

namespace GetYourPlaceApp.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<GYPUser> LoginAsync(LoginRequest loginRequest)
        {
            return new GYPUser
            {
                Email = "Tharso_rech@hotmail.com",
                IsLogged = true,
                PersonRegisterNumber = "03029666093",
                Token = Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString(),
                Address = new GYPAddress
                {
                    Address = "test",
                    City = "test",
                    Country = "test",
                    Id = 1,
                    PostalCode = "test",
                    State = "test",
                    Street = "test",
                    GYPProfilePlaceHistoryId = 1,
                    GYPUserProfileId = 1,
                }
            };
            try
            {
                var client = new RestClient(Urls.GetYourPlaceURL);
                var request = new RestRequest("Establishment/authenticate", Method.Post);
                request.AddJsonBody(new { email = loginRequest.Email, passwordHash = loginRequest.PasswordHash.MD5Hash() });

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<Result<GYPUser>>(response.Content);
                    return result?.data;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
