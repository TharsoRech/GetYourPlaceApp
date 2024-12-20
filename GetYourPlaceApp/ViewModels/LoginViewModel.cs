using CommunityToolkit.Maui.Alerts;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Contracts;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Repository.Login;
using Microsoft.Maui.Controls;
using System.Text;

namespace GetYourPlaceApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel, IDisposable
    {

        #region Properties
        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        #endregion


        public LoginViewModel()
        {
         
        }
        public void Dispose()
        {

        }


        [RelayCommand]
        public async Task Login()
        {
            LoadingPopUpPage loadingPopUpPage = new LoadingPopUpPage();

            try
            {
                loadingPopUpPage.ShowLoading();
                var loginRequest = new LoginRequest(Email, Password);

                var contract = new LoginContract(loginRequest);

                if (!contract.IsValid)
                {
                    var messages = contract.Notifications.Select(x => x.Message);
                    var sb = new StringBuilder();

                    foreach (var message in messages)
                        sb.Append($"{message}\n");

                    await Shell.Current.DisplayAlert("Atenção", sb.ToString(), "OK");
                    return;
                }

                var result = await SessionHelper.Instance.LoginAsync(loginRequest);

                if (result is null || string.IsNullOrEmpty(result.Token))
                {
                    var toast = Toast.Make("Falha ao realizar login, tenta novamente!", CommunityToolkit.Maui.Core.ToastDuration.Long);
                    await toast.Show();
                    return;
                }

                SessionHelper.Instance.User = result;
                SessionHelper.Instance.SaveToken();

                await RouteHelpers.GoToPage($"//{nameof(MainPage)}");
            }
            catch (Exception ex)
            {

               Console.WriteLine(ex.ToString());    
            }
            finally
            {
                loadingPopUpPage.HideLoading();
            }

        }
    }
}
