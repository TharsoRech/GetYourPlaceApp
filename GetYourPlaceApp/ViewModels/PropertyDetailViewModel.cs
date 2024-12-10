using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Properties;
using GetYourPlaceApp.Services.BackGroundTask;

namespace GetYourPlaceApp.ViewModels
{
    public partial class PropertyDetailViewModel:BaseViewModel
    {
        #region variables
        private static IPropertiesRepository _PropertiesRepository;
        private static INavigation _Navigation;
        BackgroundTaskRunner<Property> _getPropertiesDetailsTask;
        #endregion

        #region Properties
        [ObservableProperty]
        Property property;

        [ObservableProperty]
        bool isLoading;


        [ObservableProperty]
        bool interestedButtonDisabled;

        #endregion

        public PropertyDetailViewModel(Property property,INavigation navigation)
        {
            Property = property;
            _Navigation = navigation;

            if (_PropertiesRepository is null)
                _PropertiesRepository = ServiceHelper.GetService<IPropertiesRepository>();

            GetPropertyInfo();

        }

        public void GetPropertyInfo()
        {
            IsLoading = true;

            try
            {
                _getPropertiesDetailsTask = new BackgroundTaskRunner<Property>();
                _getPropertiesDetailsTask.StatusChanged += (serder, e) =>
                {
                    if (e.TaskStatus == BackgroundTaskStatus.Completed && e.Result != null)
                    {

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Property = e.Result;
                            CheckIsUnderAnalysis();
                            IsLoading = false;
                        });
                    }
                    else if (e.TaskStatus == BackgroundTaskStatus.Failed ||
                       (e.TaskStatus == BackgroundTaskStatus.Completed &&
                        e.Result is null))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            IsLoading = false;
                        });
                    }
                };
                _getPropertiesDetailsTask.RunInBackground(async () => await _PropertiesRepository.GetPropertyInfo(Property));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                });
            }
        }

        [RelayCommand]
        public async Task SendInsterestedToOwner()
        {
            Application.Current.MainPage.ShowPopupAsync(new MessagePopUpComponent(
                "Message sent",
                "We will make sure that the owner will receive the message",
                "Ok",
                new Command(() => SetPropertyUnderAnalysis())));
        }

        public void SetPropertyUnderAnalysis()
        {
            if (Property != null)
            {
                Property.UnderAnalysis = true;
                CheckIsUnderAnalysis();
            }

        }

        public void CheckIsUnderAnalysis()
        {
                if (Property != null)
                    InterestedButtonDisabled = Property.UnderAnalysis;
        }
    }
}
