using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.ViewModels;

public partial class MainViewModel : BaseViewModel, IDisposable
{
    #region variables
    #endregion

    #region Properties
    [ObservableProperty]
    ObservableCollection<GYPFilterItem> filters;

    [ObservableProperty]
    bool filtersApplied;
    #endregion
    public MainViewModel()
    {
        FilterManager.Instance.FilterUpdated += FilterUpdated;
    }
    [RelayCommand]
    public async Task ShowFilter()
    {

        await Application.Current.MainPage.ShowPopupAsync(new FilterPopUp());

    }

    public void Dispose()
    {
        FilterManager.Instance.FilterUpdated -= FilterUpdated;
    }

    private void FilterUpdated(object sender,List<GYPFilterItem> filterItems)
    {
        if(filterItems != null)
        {
            Filters = new ObservableCollection<GYPFilterItem>(filterItems);
            FiltersApplied = filterItems.Any();
        }
    }
}
