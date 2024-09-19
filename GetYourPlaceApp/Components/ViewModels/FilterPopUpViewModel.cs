using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Filter;
using System.Collections.ObjectModel;

namespace GetYourPlaceApp.Components.ViewModels
{
    public partial class FilterPopUpViewModel:BaseViewModel
    {

        #region variables
        Popup _popup;
        IFilterRepository _filterRepository;
        #endregion

        #region Properties
        [ObservableProperty]
        ObservableCollection<GYPFilter> pickerList;
        #endregion

        public FilterPopUpViewModel(Popup popup)
        {
            _filterRepository = ServiceHelper.GetService<IFilterRepository>(); 
            _popup = popup;
            Task.Run(async () => {
                pickerList = new ObservableCollection<GYPFilter>(await _filterRepository.GetFilters());;
            }).Wait();
        }
        [RelayCommand]
        public async Task ApplyFilters()
        {
            await _popup.CloseAsync();

        }

        [RelayCommand]
        public async Task Cancel()
        {
            await _popup.CloseAsync();
        }
    }
}
