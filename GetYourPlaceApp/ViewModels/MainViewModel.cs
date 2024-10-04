using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Filter;
using GetYourPlaceApp.Repository.Properties;

namespace GetYourPlaceApp.ViewModels;

public partial class MainViewModel : BaseViewModel, IDisposable
{
    #region variables
    private static IPropertiesRepository _PropertiesRepository;
    #endregion

    #region Properties
    [ObservableProperty]
    ObservableCollection<GYPPropertyInfoItem> filters;

    [ObservableProperty]
    ObservableCollection<Property> currentProperties;

    [ObservableProperty]
    bool propertysLoading;

    [ObservableProperty]
    bool filtersApplied;
    #endregion
    public MainViewModel() { 

        if (_PropertiesRepository is null)
            _PropertiesRepository = ServiceHelper.GetService<IPropertiesRepository>();

        FilterManager.Instance.FilterUpdated += FilterUpdated;

        GetProperties();
    }

    public async Task GetProperties()
    {
        PropertysLoading = true;
        try
        {
            Task.Run(async () =>
            {
                Task.Delay(2000);
                CurrentProperties = new ObservableCollection<Property>(await _PropertiesRepository.GetProperties());
            }).Wait();
        }
        catch (Exception ex)
        {

           Console.WriteLine(ex.ToString());    
        }
        finally
        {
            PropertysLoading = false;
        }

    }
    [RelayCommand]
    public async Task ShowFilter()
    {

        Application.Current.MainPage.ShowPopupAsync(new FilterPopUp());

    }

    [RelayCommand]
    public async Task ShowEditFilter()
    {

        Application.Current.MainPage.ShowPopupAsync(new FilterEditPopUp());
    }

    public void Dispose()
    {
        FilterManager.Instance.FilterUpdated -= FilterUpdated;
    }

    private void FilterUpdated(object sender,List<GYPPropertyInfoItem> filterItems)
    {
        if(filterItems != null)
        {
            Filters = new ObservableCollection<GYPPropertyInfoItem>(filterItems);
            FiltersApplied = filterItems.Any();

            GetProperties();
        }
    }
}
