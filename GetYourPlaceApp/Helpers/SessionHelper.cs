using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Repository.Login;
using System.Security.Cryptography;
using System.Text;

namespace GetYourPlaceApp.Helpers
{
    public sealed class SessionHelper
    {
        #region Variables
        public event EventHandler<bool> SessionChanged;
        public GYPUser User;
        private static ILoginRepository _loginRepository;
        private static SessionHelper instance = null;
        #endregion

        public static SessionHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionHelper();
                }
                return instance;
            }
        }

        private SessionHelper()
        {
            
        }
        public async Task<GYPUser> LoginAsync(LoginRequest loginRequest)
        {
            GYPUser user = null;
            try
            {
                if(_loginRepository is null)
                    _loginRepository = ServiceHelper.GetService<ILoginRepository>();

               user = await _loginRepository.LoginAsync(loginRequest);
                await Task.Delay(5000);
                Preferences.Set("token", User?.Token);
                Preferences.Set("ExpireDateTimeKey", User?.RefreshToken);

                if(User != null)
                    User.IsLogged = true;

                SessionChanged?.Invoke(null, true);
            }
            catch (Exception ex)
            {
                return user;
            }
            return user;
        }

        public async Task<string> GetTokenAsync()
        {
            var expireDateTime = Preferences.Get("ExpireDateTimeKey", DateTime.MinValue);
            string token = Preferences.Get("token", string.Empty);

            if (expireDateTime <= DateTime.Now)
            {
                LogoffAsync();
                return string.Empty;
            }

            return token;
        }

        public  void ResetToken()
        {
            Preferences.Set("token", null);
            Preferences.Set("ExpireDateTimeKey", null);
            User.IsLogged = false;
            SessionChanged?.Invoke(null, false);
        } 

        public  void SaveToken()
        {
            Preferences.Set("token", User.Token);
            Preferences.Set("ExpireDateTimeKey", User.RefreshToken);
        }

        public void LogoffAsync()
        {
            try
            {
                ResetToken();
            }
            catch (Exception ex)
            {

            }
        }

    }
}
