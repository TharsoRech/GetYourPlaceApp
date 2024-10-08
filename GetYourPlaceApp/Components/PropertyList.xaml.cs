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

    public static readonly BindableProperty PropertysProperty = BindableProperty.Create(
        nameof(Propertys),
        typeof(ObservableCollection<Property>),
        typeof(PropertyList)
        );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(PropertyList)
    );

    #endregion

    #region [Properties]

    public ObservableCollection<Property> Propertys
    {
        get => (ObservableCollection<Property>)this.GetValue(PropertysProperty);
        set => this.SetValue(PropertysProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }
    #endregion

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