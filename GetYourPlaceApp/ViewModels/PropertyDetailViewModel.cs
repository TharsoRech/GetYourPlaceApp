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
        BackgroundTaskRunner<Property> _getPropertiesDetailsTask;
        #endregion

        #region Properties
        [ObservableProperty]
        Property property;

        [ObservableProperty]
        bool isLoading;
        #endregion

        public PropertyDetailViewModel(Property property)
        {
            Property = property;

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


    }
}
