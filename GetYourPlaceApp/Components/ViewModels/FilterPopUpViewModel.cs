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

        ObservableCollection<GYPPropertyInfoItem> filterList;
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

            LoadFiltersApplied();
        }

        private void LoadFiltersApplied()
        {
            try
            {
                var currentFilters = FilterManager.Instance.GetFilters();

                if (currentFilters?.Count > 0 && pickerList?.Count > 0)
                {
                    foreach (var filter in currentFilters)
                    {
                        var picker = pickerList.FirstOrDefault(f =>
                        f.GYPPropertyInfo == filter.GYPPropertyInfo);

                        var pickerItem = picker.Items.FirstOrDefault(i =>
                        i.Id == filter.Id &&
                        i.GYPPropertyInfo == filter.GYPPropertyInfo);

                        if (picker != null && pickerItem != null)
                            picker.SelectedIndexFilter = picker.Items.IndexOf(pickerItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        public void UpdateFilters(GYPPropertyInfoItem filterItem)
        {
            if(filterItem != null)
            {
                var filterFound = filterList?.FirstOrDefault(f => 
                f.GYPPropertyInfo == filterItem.GYPPropertyInfo);

                if (filterFound != null)
                    filterList?.Remove(filterFound);

                if(filterList is null)
                   filterList = new ObservableCollection<GYPPropertyInfoItem> { filterItem };   
                else
                    filterList.Add(filterItem);
;
            }
        }

    }
}
