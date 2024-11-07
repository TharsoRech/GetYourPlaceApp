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
    BackgroundTaskRunner<bool> _applyFilterTask; 
    #endregion

    #region Properties
    [ObservableProperty]
    ObservableCollection<GYPPropertyInfoItem> filters;

    [ObservableProperty]
    ObservableCollection<Property> currentProperties;

    [ObservableProperty]
    ObservableCollection<Property> allProperties;

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

    public async Task GetPropertiesInBackground(bool getProperties = true,Tuple<int, int> pageInfoItem =  null)
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
                    if (getProperties)
                        AllProperties = new ObservableCollection<Property>(e.Result);

                    ApplyPropertiesByOrder(FilterManager.Instance.CurrentFilterSelected);

                    CurrentProperties = AllProperties;

                    if (pageInfoItem != null)
                    {
                        var itemsToSkip = AllProperties.Skip((pageInfoItem.Item2 *
                            pageInfoItem.Item1) - pageInfoItem.Item2);
                        CurrentProperties = new ObservableCollection<Property>
                            (itemsToSkip.Take(pageInfoItem.Item2).ToList());
                    }
                    else
                        CurrentProperties = new ObservableCollection<Property>(CurrentProperties.Take(4).ToList());

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        PropertiesLoading = false;
                    });
                }
                else if(e.TaskStatus == BackgroundTaskStatus.Failed || 
                   (e.TaskStatus == BackgroundTaskStatus.Completed && 
                    e.Result is null))
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        PropertiesLoading = false;
                    });
                }
            };
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

    [RelayCommand]
    public async Task PageChanged(Tuple<int, int> pageInfoItem)
    {
        GetPropertiesInBackground(false,pageInfoItem);
    }

    [RelayCommand]
    public async Task OrderUpdated(string filterItem)
    {
        FilterManager.Instance.RaiseChangeOrderEvent(filterItem);
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
        GetPropertiesInBackground();
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
            GetPropertiesInBackground(false);
    }

    private void FilterUpdated(object sender,List<GYPPropertyInfoItem> filterItems)
    {
        if(filterItems != null)
        {
            Filters = new ObservableCollection<GYPPropertyInfoItem>(filterItems);
            FiltersApplied = filterItems.Any();

            GetPropertiesInBackground();
        }
    }

    public async Task<bool> ApplyPropertiesByOrder(string filterItem)
    {  
        try
        {
            if(filterItem != null)
            {
                if (filterItem.Contains("Most Recent (High to Low)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderByDescending(p => p.PuplishedAt).ToList());

                if (filterItem.Contains("Most Recent (Low to High)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderBy(p => p.PuplishedAt).ToList());

                if (filterItem.Contains("Rating (High to Low)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderByDescending(p => p.Star).ToList());

                if (filterItem.Contains("Rating (Low to High)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderBy(p => p.Star).ToList());

                if (filterItem.Contains("Price (High to Low)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderByDescending(p => p.Price).ToList());

                if (filterItem.Contains("Price (Low to High)"))
                    AllProperties = new ObservableCollection<Property>(AllProperties.OrderBy(p => p.Price).ToList());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }
}
