using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components.ViewModels;
using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class FilterPopUp : Popup
{
    FilterPopUpViewModel _VM;
    public FilterPopUp()
	{
		InitializeComponent();
        _VM = new FilterPopUpViewModel(this);
        BindingContext = _VM;
    }

    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                GYPPropertyInfoItem filterItem = (GYPPropertyInfoItem)picker.ItemsSource[selectedIndex];
                _VM.UpdateFilters(filterItem);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());   
        }
   
    }
}
