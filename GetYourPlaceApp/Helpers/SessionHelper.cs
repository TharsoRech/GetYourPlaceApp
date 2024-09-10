using GetYourPlaceApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace GetYourPlaceApp.Helpers
{
    public static class SessionHelper
    {
        public static event EventHandler<bool> SessionChanged;
        public static GYPUser User;
        public static bool Login(string username, string password)
        {
            if(User is null)
                return false;

            Preferences.Set("token", User?.Token);
            Preferences.Set("ExpireDateTimeKey", User?.RefreshToken);
            User.IsLogged = true;
            SessionChanged?.Invoke(null, true);
            return true;
        }

        public static async Task<string> GetTokenAsync()
        {
            var expireDateTime = Preferences.Get("ExpireDateTimeKey", DateTime.MinValue);
            string token = Preferences.Get("token", string.Empty);

            if (expireDateTime <= DateTime.Now)
            {
                await RouteHelpers.LogoffAsync();
                return string.Empty;
            }

            return token;
        }

        public static void ResetToken()
        {
            Preferences.Set("token", null);
            Preferences.Set("ExpireDateTimeKey", null);
            User.IsLogged = false;
            SessionChanged?.Invoke(null, false);
        } 

        public static void SaveToken()
        {
            Preferences.Set("token", User.Token);
            Preferences.Set("ExpireDateTimeKey", User.RefreshToken);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));


            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
