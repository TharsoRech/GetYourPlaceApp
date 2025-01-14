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
    public partial class ProfileViewModel:BaseViewModel
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
        string imageProfile;

        [ObservableProperty]
        Country countrySelected;

        [ObservableProperty]
        State stateSelected;

        [ObservableProperty]
        string citySelected;

        [ObservableProperty]
        ObservableCollection<string> citys;

        [ObservableProperty]
        ObservableCollection<Country> countrys;

        [ObservableProperty]
        ObservableCollection<State> states;

        #endregion
        public ProfileViewModel(INavigation navigation)
        {
            _Navigation = navigation;

            if (_locationRepository is null)
                _locationRepository = ServiceHelper.GetService<ILocationRepository>();

            Task.Run(async () => {
                Countrys = new ObservableCollection<Country>(await _locationRepository.GetCountriesAndStates());
                Country = SessionHelper.Instance.User.Address.Country;
                CountrySelected = Countrys?.FirstOrDefault(c => c.name == SessionHelper.Instance.User.Address.Country);

                await LoadState(false);

            });

            FullName = SessionHelper.Instance.User.FullName;
            Email = SessionHelper.Instance.User.Email;
            StreetAdress = SessionHelper.Instance.User.Address.Street;
            RegistrationNumber = SessionHelper.Instance.User.PersonRegisterNumber;
            ZipCode = SessionHelper.Instance.User.Address.ZipCode;
            AddressComplement = SessionHelper.Instance.User.Address.AddressComplement;
            ImageProfile = SessionHelper.Instance.User.Address.ImageProfileBase64 ?? "profile.svg";

        }

        [RelayCommand]
        public async Task CreateAccount()
        {
            Application.Current.MainPage.ShowPopupAsync(new MessagePopUpComponent(
              "Account Created",
              "Thanks for creating a account , now you can login in your account",
              "Ok",
              null));
        }

        [RelayCommand]
        public async Task LoadImageForProfile()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (status != PermissionStatus.Granted)
                {
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        status = await Permissions.RequestAsync<Permissions.StorageRead>();
                    });
                }

                // If permission was granted, show file picker
                if (status == PermissionStatus.Granted)
                {
                    // Use the FilePicker to select an image
                    var result = await FilePicker.PickAsync(new PickOptions()
                    {
                        PickerTitle = "Please select an image",
                        FileTypes = FilePickerFileType.Images
                    });

                    // Display the image
                    using var stream = await result.OpenReadAsync();


                    // Convert image to Base64
                    var base64String = await ConvertImageToBase64(stream);
                    ImageProfile = $"data:image/png;base64,{base64String}"; ; // Display the Base64 string
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<string> ConvertImageToBase64(Stream imageStream)
        {
            using var memoryStream = new MemoryStream();
            await imageStream.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            return Convert.ToBase64String(imageBytes);
        }

        public async Task LoadState(bool userSelected = true)
        {
            States = Countrys?.FirstOrDefault(c => c.name == Country)?.states?.ToObservableCollection();

            if (!userSelected)
            {
                State = SessionHelper.Instance.User.Address.State;
                StateSelected = States?.FirstOrDefault(states => states.name == State);
            }


        }

        public async Task LoadCitys(bool userSelected = true)
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

            if (!userSelected)
                CitySelected = SessionHelper.Instance.User.Address.City;

            loading.HideLoading();
        }
    }
}
