using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;

namespace GetYourPlaceApp.ViewModels
{
    public partial class NewAccountViewModel:BaseViewModel
    {
        #region variables
        private static INavigation _Navigation;
        #endregion

        #region Properties

        [ObservableProperty]
        string fullName;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string repeatPassword;

        [ObservableProperty]
        string streetAdress;

        [ObservableProperty]
        string city;

        [ObservableProperty]
        string country;

        [ObservableProperty]
        string registrationNumber;

        [ObservableProperty]
        string zipCode;

        [ObservableProperty]
        string addressComplement;

        #endregion
        public NewAccountViewModel(INavigation navigation)
        {
            _Navigation = navigation;
        }

        [RelayCommand]
        public async Task CreateAccount()
        {
            Application.Current.MainPage.ShowPopupAsync(new MessagePopUpComponent(
              "Account Created",
              "Thanks for create a account , now you can login in your account",
              "Ok",
              new Command(() => GoToLoginAsync())));
        }

        public async Task GoToLoginAsync()
        {
            var loading = new LoadingPopUpPage();
            loading.ShowLoading();
            await _Navigation.PushAsync(new LoginPage());
            loading.HideLoading();
        }
    }
}

