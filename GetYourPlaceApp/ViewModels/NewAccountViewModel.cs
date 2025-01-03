using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Models.Requests;
using GetYourPlaceApp.Models.Responses;
using GetYourPlaceApp.Repository.Location;

namespace GetYourPlaceApp.ViewModels
{
    public partial class NewAccountViewModel:BaseViewModel
    {
        #region variables
        private static INavigation _Navigation;
        private static ILocationRepository _locationRepository;
        private CountryAndStateResponse _countryAndStateResponse;
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

        [ObservableProperty]
        string state;

        [ObservableProperty]
        ObservableCollection<string> citys;

        [ObservableProperty]
        ObservableCollection<Country> countrys;

        [ObservableProperty]
        ObservableCollection<State> states;

        #endregion
        public NewAccountViewModel(INavigation navigation)
        {
            _Navigation = navigation;

            if (_locationRepository is null)
                _locationRepository = ServiceHelper.GetService<ILocationRepository>();

            Task.Run(async () => {
                Countrys = new ObservableCollection<Country>(await _locationRepository.GetCountriesAndStates());
            }).Wait();

        }

        [RelayCommand]
        public async Task CreateAccount()
        {
            Application.Current.MainPage.ShowPopupAsync(new MessagePopUpComponent(
              "Account Created",
              "Thanks for creating a account , now you can login in your account",
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

        public async Task LoadState()
        {
            States = Countrys?.FirstOrDefault(c => c.name == Country)?.states?.ToObservableCollection();
        }

        public async Task LoadCitys()
        {
            var loading = new LoadingPopUpPage();
            loading.ShowLoading();
            var cityRequest = new CityRequest
            {
                country = Country,
                state = State,
            };
            var citys = await _locationRepository.GetCityByState(cityRequest);
            Citys = citys?.ToObservableCollection();
            loading.HideLoading();
        }
    }
}

