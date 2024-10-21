using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Properties;
using GetYourPlaceApp.Services.BackGroundTask;

namespace GetYourPlaceApp.ViewModels;

public partial class MainViewModel : BaseViewModel, IDisposable
{
    #region variables
    private static IPropertiesRepository _PropertiesRepository;
    BackgroundTaskRunner<List<Property>> _getPropertiesTask;
    #endregion

    #region Properties
    [ObservableProperty]
    ObservableCollection<GYPPropertyInfoItem> filters;

    [ObservableProperty]
    ObservableCollection<Property> currentProperties;

    [ObservableProperty]
    bool propertiesLoading;

    [ObservableProperty]
    bool filtersApplied;

    [ObservableProperty]
    string searchText;
    #endregion
    public MainViewModel() { 

        if (_PropertiesRepository is null)
            _PropertiesRepository = ServiceHelper.GetService<IPropertiesRepository>();

        FilterManager.Instance.FilterUpdated += FilterUpdated;
        FilterManager.Instance.FilterChangeOrder += FilterOrderUpdated;
    }

    public async Task GetProperties()
    {
        PropertiesLoading = true;
 
        try
        {
            _getPropertiesTask =  new BackgroundTaskRunner<List<Property>>();
            _getPropertiesTask.RunInBackground(async () => await _PropertiesRepository.GetProperties());
            _getPropertiesTask.StatusChanged += (serder, e) =>
            {
                if (e.TaskStatus == BackgroundTaskStatus.Completed && e.Result != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CurrentProperties = new ObservableCollection<Property>(e.Result);

                        if (FilterManager.Instance.CurrentFilterSelected != null)
                            ApplyPropertiesByOrder(FilterManager.Instance.CurrentFilterSelected);

                    });
                }
            };
        }
        catch (Exception ex)
        {

           Console.WriteLine(ex.ToString());    
        }
        finally
        {
            PropertiesLoading = false;
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

    [RelayCommand]
    public async Task SearchPropertie()
    {
        FilterManager.Instance.CustomFilter = SearchText;
        GetProperties();
    }

    public void Dispose()
    {
        _getPropertiesTask?.Dispose();
        _getPropertiesTask = null;
        FilterManager.Instance.FilterUpdated -= FilterUpdated;
        FilterManager.Instance.FilterChangeOrder -= FilterOrderUpdated;
    }

    private void FilterOrderUpdated(object sender, string filter)
    {
        if (filter != null)
            ApplyPropertiesByOrder(filter);
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

    public void ApplyPropertiesByOrder(string filterItem)
    {
        PropertiesLoading = true;
        try
        {
            if (filterItem.Contains("Most Recent (High to Low)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderByDescending(p => p.PuplishedAt).ToList());

            if (filterItem.Contains("Most Recent (Low to High)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderBy(p => p.PuplishedAt).ToList());

            if (filterItem.Contains("Rating (High to Low)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderByDescending(p => p.Star).ToList());

            if (filterItem.Contains("Rating (Low to High)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderBy(p => p.Star).ToList());

            if (filterItem.Contains("Price (High to Low)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderByDescending(p => p.Price).ToList());

            if (filterItem.Contains("Price (Low to High)"))
                CurrentProperties = new ObservableCollection<Property>(CurrentProperties.OrderBy(p => p.Price).ToList());

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        { PropertiesLoading = false; }
    }
}
