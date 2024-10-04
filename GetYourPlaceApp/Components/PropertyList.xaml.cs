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
                   ApplyPropertiesByOrder(filterItem);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public void ApplyPropertiesByOrder(string filterItem)
    {
        IsLoading = true;
        try
        {
            if(filterItem.Contains("Most Recent (High to Low)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderByDescending(p => p.PuplishedAt).ToList());

            if (filterItem.Contains("Most Recent (Low to High)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderBy(p => p.PuplishedAt).ToList());

            if (filterItem.Contains("Rating (High to Low)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderByDescending(p => p.Star).ToList());

            if (filterItem.Contains("Rating (Low to High)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderBy(p => p.Star).ToList());

            if (filterItem.Contains("Price (High to Low)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderByDescending(p => p.Price).ToList());

            if (filterItem.Contains("Price (Low to High)"))
                Propertys = new ObservableCollection<Property>(Propertys.OrderBy(p => p.Price).ToList());

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        { IsLoading = false; }
    }
}