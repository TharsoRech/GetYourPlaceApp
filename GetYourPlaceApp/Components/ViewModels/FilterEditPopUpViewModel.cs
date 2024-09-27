using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components.ViewModels
{
    public partial class FilterEditPopUpViewModel: BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        ObservableCollection<GYPFilterItem> filters;
        #endregion

        #region Variable
        Popup _Popup;
        #endregion

        public FilterEditPopUpViewModel(Popup popup)
        {
            _Popup = popup; 
            Filters = new ObservableCollection<GYPFilterItem>(FilterManager.Instance.GetFilters());
        }

        [RelayCommand]
        public async Task ApplyFilters()
        {
            FilterManager.Instance.RemoveFilters(Filters?.ToList());
            await _Popup.CloseAsync();
        }

        [RelayCommand]
        public async Task Cancel()
        {
            await _Popup.CloseAsync();
        }

        [RelayCommand]
        public async Task RemoveFilter(GYPFilterItem filterItem)
        {
            Filters?.Remove(filterItem);
        }
    }
}
