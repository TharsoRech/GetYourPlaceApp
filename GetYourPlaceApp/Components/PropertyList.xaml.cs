using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class PropertyList : ContentView
{
    public PropertyList()
	{
		InitializeComponent();

    }

    #region Bindable Properties

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
    nameof(Items),
    typeof(ObservableCollection<Property>),
    typeof(PropertyList)
    );

    public static readonly BindableProperty AllItemsProperty = BindableProperty.Create(
    nameof(AllItems),
    typeof(ObservableCollection<Property>),
    typeof(PropertyList)
    );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(PropertyList)
    );

    public static readonly BindableProperty PageChangedCommandProperty = BindableProperty.Create(
    nameof(PageChangedCommand),
    typeof(IAsyncRelayCommand),
    typeof(PropertyList)
    );

    public static readonly BindableProperty OrderUpdatedCommandProperty = BindableProperty.Create(
    nameof(OrderUpdatedCommand),
    typeof(IAsyncRelayCommand),
    typeof(PropertyList)
    );

    #endregion

    #region [Properties]

    public ObservableCollection<Property> Items
    {
        get => (ObservableCollection<Property>)this.GetValue(ItemsProperty);
        set => this.SetValue(ItemsProperty, value);
    }

    public ObservableCollection<Property> AllItems
    {
        get => (ObservableCollection<Property>)this.GetValue(AllItemsProperty);
        set => this.SetValue(AllItemsProperty, value);
    }

    public IAsyncRelayCommand PageChangedCommand
    {
        get => (IAsyncRelayCommand)this.GetValue(PageChangedCommandProperty);
        set => this.SetValue(PageChangedCommandProperty, value);
    }

    public IAsyncRelayCommand OrderUpdatedCommand
    {
        get => (IAsyncRelayCommand)this.GetValue(OrderUpdatedCommandProperty);
        set => this.SetValue(OrderUpdatedCommandProperty, value);
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
        PageChangedCommand?.ExecuteAsync(pageInfoItem);
    }

    [RelayCommand]
    public async Task PreviousPage(Tuple<int, int> pageInfoItem)
    {
        PageChangedCommand?.ExecuteAsync(pageInfoItem);
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

                if (filterItem != null)
                    OrderUpdatedCommand?.ExecuteAsync(filterItem);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
}