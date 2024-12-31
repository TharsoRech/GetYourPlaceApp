namespace GetYourPlaceApp.Views;

public partial class NewAccountPage : ContentPage
{
	NewAccountViewModel _vm;
    public NewAccountPage()
	{
		InitializeComponent();
		BindingContext = _vm= new NewAccountViewModel(Navigation);
	}
}