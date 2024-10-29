using GetYourPlaceApp.Components.ViewModels;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;
using System.Linq;

namespace GetYourPlaceApp.Components;

public partial class PropertyList : ContentView
{
    public PropertyList()
	{
		InitializeComponent();

    }

    #region Bindable Properties

    public static readonly BindableProperty AllPropertiesProperty = BindableProperty.Create(
        nameof(AllProperties),
        typeof(ObservableCollection<Property>),
        typeof(PropertyList),
        propertyChanged: PropertyUpdated
        );

    private static readonly BindableProperty CurrentPropertiesProperty = BindableProperty.Create(
    nameof(CurrentProperties),
    typeof(ObservableCollection<Property>),
    typeof(PropertyList)
    );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(PropertyList)
    );

    private static void PropertyUpdated(BindableObject bindable, object oldValue, object newValue)
    {
        var paginationComponent = (PropertyList)bindable;
        paginationComponent.UpdateList(paginationComponent.AllProperties);
    }

    #endregion

    #region [Properties]

    public ObservableCollection<Property> AllProperties
    {
        get => (ObservableCollection<Property>)this.GetValue(AllPropertiesProperty);
        set => this.SetValue(AllPropertiesProperty, value);
    }

    public ObservableCollection<Property> CurrentProperties
    {
        get => (ObservableCollection<Property>)this.GetValue(CurrentPropertiesProperty);
        set => this.SetValue(CurrentPropertiesProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }

    #endregion

    [RelayCommand]
    public async Task NextPage(Tuple<int,int> pageInfoItem)
    {
        CurrentProperties = new ObservableCollection<Property>
            (AllProperties.Skip(pageInfoItem.Item2 * pageInfoItem.Item1).
            Take(pageInfoItem.Item2).ToList());
    }

    [RelayCommand]
    public async Task PreviousPage(Tuple<int, int> pageInfoItem)
    {
        CurrentProperties = new ObservableCollection<Property>
            (AllProperties.Skip((pageInfoItem.Item2 -1) * pageInfoItem.Item1)
            .Take(pageInfoItem.Item2).ToList());
    }

    public void UpdateList(ObservableCollection<Property> properties)
    {
        CurrentProperties = new ObservableCollection<Property>
            (AllProperties.Take(4).ToList());
    }
    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var filterItem = (string)picker.ItemsSource[selectedIndex];

                if(filterItem != null)
                    FilterManager.Instance.RaiseChangeOrderEvent(filterItem);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}