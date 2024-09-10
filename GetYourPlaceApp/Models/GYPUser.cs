namespace GetYourPlaceApp.Models
{
    public class GYPUser
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string PersonRegisterNumber { get; set; }
        public GYPAddress Address { get; set; }
        public bool IsLogged { get; set; }
    }
}
