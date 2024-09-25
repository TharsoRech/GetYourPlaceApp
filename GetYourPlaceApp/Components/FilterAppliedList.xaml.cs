using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class FilterAppliedList : ContentView
{
	public FilterAppliedList()
	{
		InitializeComponent();
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
        typeof(FilterAppliedList),
        defaultBindingMode: BindingMode.TwoWay
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
        FilterItems?.Remove(filterItem);
        FilterManager.Instance.RemoveFilter(filterItem);
    }
}