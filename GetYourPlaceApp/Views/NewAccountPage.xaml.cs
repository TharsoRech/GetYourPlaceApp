using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Views;

public partial class NewAccountPage : ContentPage
{
	NewAccountViewModel _vm;
    public NewAccountPage()
	{
		InitializeComponent();
		BindingContext = _vm= new NewAccountViewModel(Navigation);
	}

    public void OnPickerCountrySelected(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var filterItem = (Country)picker.ItemsSource[selectedIndex];

                if (filterItem != null)
                {
                    _vm.Country = filterItem.name;
                    _vm.LoadState();
                }
 
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public void OnPickerStateSelected(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var filterItem = (State)picker.ItemsSource[selectedIndex];

                if (filterItem != null)
                {
                    _vm.State = filterItem.name;
                    _vm.LoadCitys();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public void OnPickerCitySelected(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var filterItem = (string)picker.ItemsSource[selectedIndex];

                if (filterItem != null)
                    _vm.City = filterItem;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }



}