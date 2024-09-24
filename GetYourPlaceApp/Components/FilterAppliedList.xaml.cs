using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class FilterAppliedList : ContentView, IDisposable
{
	public FilterAppliedList()
	{
		InitializeComponent();
        FilterManager.Instance.FilterUpdated += FilterUpdated;
    }

    #region Bindable Properties

    public static readonly BindableProperty FilterItemsProperty = BindableProperty.Create(
        nameof(FilterItems),
        typeof(ObservableCollection<GYPFilterItem>),
        typeof(FilterAppliedList),
        defaultValue: null
        );


    public static readonly BindableProperty HasFilterItemsProperty = BindableProperty.Create(
        nameof(HasFilterItems),
        typeof(bool),
        typeof(FilterAppliedList)
        );

    #endregion

    #region [Properties]

    public ObservableCollection<GYPFilterItem> FilterItems
    {
        get => (ObservableCollection<GYPFilterItem>)this.GetValue(FilterItemsProperty);
        set => this.SetValue(FilterItemsProperty, value);
    }

    public bool HasFilterItems
    {
        get => (bool)this.GetValue(HasFilterItemsProperty);
        set => this.SetValue(HasFilterItemsProperty, value);
    }
    #endregion

    [RelayCommand]
    public async Task RemoveFilter(GYPFilterItem filterItem)
    {
        FilterItems.Remove(filterItem);
        FilterManager.Instance.RemoveFilter(filterItem);
    }

    private void FilterUpdated(object sender, List<GYPFilterItem> filterItems)
    {
        HasFilterItems = FilterItems?.Count > 0;
    }

    public void Dispose()
    {
        FilterManager.Instance.FilterUpdated -= FilterUpdated;
    }
}