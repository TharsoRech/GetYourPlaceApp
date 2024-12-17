using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Properties;
using GetYourPlaceApp.Services.BackGroundTask;

namespace GetYourPlaceApp.ViewModels
{
    public partial class FavoritesViewModel:BaseViewModel
    {
        #region variables
        private static IPropertiesRepository _PropertiesRepository;
        BackgroundTaskRunner<List<Property>> _getPropertiesLikedTask;
        #endregion

        #region Properties

        [ObservableProperty]
        bool propertiesLoading;

        [ObservableProperty]
        ObservableCollection<Property> likedProperties;

        #endregion
        public FavoritesViewModel()
        {
            if (_PropertiesRepository is null)
                _PropertiesRepository = ServiceHelper.GetService<IPropertiesRepository>();

        }

        public async Task GetPropertiesInBackground()
        {
            PropertiesLoading = true;

            try
            {
                _getPropertiesLikedTask = new BackgroundTaskRunner<List<Property>>();
                _getPropertiesLikedTask.StatusChanged += (serder, e) =>
                {
                    if (e.TaskStatus == BackgroundTaskStatus.Completed && e.Result != null)
                    {
                        LikedProperties = new ObservableCollection<Property>(e.Result);

                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            PropertiesLoading = false;
                        });
                    }
                    else if (e.TaskStatus == BackgroundTaskStatus.Failed ||
                       (e.TaskStatus == BackgroundTaskStatus.Completed &&
                        e.Result is null))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            PropertiesLoading = false;
                        });
                    }
                };
                _getPropertiesLikedTask.RunInBackground(async () => await _PropertiesRepository.GetLikedProperties());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    PropertiesLoading = false;
                });
            }

        }
    }
}
