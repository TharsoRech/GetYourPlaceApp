namespace GetYourPlaceApp.Views;

public partial class EditPropertyPage : ContentPage
{
	EditPropertyViewModel _vm;
    public EditPropertyPage()
	{
		InitializeComponent();
		BindingContext = _vm = new EditPropertyViewModel();
	}
}