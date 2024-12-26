using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Properties;
using GetYourPlaceApp.Services.BackGroundTask;

namespace GetYourPlaceApp.ViewModels
{
    public partial class MyPropertiesViewModel:BaseViewModel
    {
        #region variables
        private static IPropertiesRepository _PropertiesRepository;
        BackgroundTaskRunner<List<Property>> _getPropertiesTask;
        #endregion

        [ObservableProperty]
        ObservableCollection<Property> allProperties;

        #region Properties
        [ObservableProperty]
        ObservableCollection<Property> propertiesPublished;

        [ObservableProperty]
        ObservableCollection<Property> propertiesUnPublished;


        [ObservableProperty]
        ObservableCollection<Property> propertiesUnderAnalysis;

        [ObservableProperty]
        ObservableCollection<Property> propertiesMatched;

        [ObservableProperty]
        ObservableCollection<Property> propertiesUnMatched;


        #endregion
        public MyPropertiesViewModel()
        {

            if (_PropertiesRepository is null)
                _PropertiesRepository = ServiceHelper.GetService<IPropertiesRepository>();

            GetPropertiesInBackground();
        }

        public async Task GetPropertiesInBackground()
        {

            try
            {
                _getPropertiesTask = new BackgroundTaskRunner<List<Property>>();
                _getPropertiesTask.StatusChanged += (serder, e) =>
                {
                    if (e.TaskStatus == BackgroundTaskStatus.Completed && e.Result != null)
                        AllProperties = new ObservableCollection<Property>(e.Result);

                };
                _getPropertiesTask.RunInBackground(async () => await _PropertiesRepository.GetProperties());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }

        }

        [RelayCommand]
        public async Task GetPublishedProperties()
        {
            PropertiesPublished = new ObservableCollection<Property>
                (AllProperties.Where(p=> 
                p.GYPUserProfileId == SessionHelper.Instance.User.Id
                && p.Published));
        }


        [RelayCommand]
        public async Task GetUnPublishedProperties()
        {
            PropertiesUnPublished = new ObservableCollection<Property>
                (AllProperties.Where(p =>
                p.GYPUserProfileId == SessionHelper.Instance.User.Id
                && !p.Published));
        }

        [RelayCommand]
        public async Task GetUnderAnalysisProperties()
        {
            PropertiesUnderAnalysis = new ObservableCollection<Property>
                (AllProperties.Where(p => p.UnderAnalysis));
        }

        [RelayCommand]
        public async Task GetMatchedProperties()
        {
            PropertiesMatched = new ObservableCollection<Property>
                (AllProperties.Where(p => p.Accepted));
        }

        [RelayCommand]
        public async Task GetUnMatchedProperties()
        {
            PropertiesUnMatched = new ObservableCollection<Property>
                (AllProperties.Where(p => p.Rejected));
        }

    }
}
