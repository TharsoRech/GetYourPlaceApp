using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;
using GetYourPlaceApp.Repository.Filter;

namespace GetYourPlaceApp.Components.ViewModels
{
    public partial class FilterPopUpViewModel:BaseViewModel
    {

        #region variables
        Popup _popup;
        IFilterRepository _filterRepository;

        ObservableCollection<GYPFilterItem> filterList;
        #endregion

        #region Properties
        [ObservableProperty]
        ObservableCollection<GYPFilter> pickerList;
        
        [ObservableProperty]
        GYPFilter filterItemSelected;

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
            FilterManager.Instance.ApplyFilters(filterList?.ToList());
            await _popup.CloseAsync();

        }

        [RelayCommand]
        public async Task Cancel()
        {
            await _popup.CloseAsync();
        }

        public void UpdateFilters(GYPFilterItem filterItem)
        {
            if(filterItem != null)
            {
                var filterFound = filterList?.FirstOrDefault(f => 
                f.GYPFilterType == filterItem.GYPFilterType);

                if (filterFound != null)
                    filterList?.Remove(filterFound);

                if(filterList is null)
                   filterList = new ObservableCollection<GYPFilterItem> { filterItem };   
                else
                    filterList.Add(filterItem);
;
            }
        }

    }
}
