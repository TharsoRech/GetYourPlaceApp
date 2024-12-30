namespace GetYourPlaceApp.Views;

public partial class ProfilePage : ContentPage
{
	ProfileViewModel _vm;
    public ProfilePage()
	{
		InitializeComponent();
		BindingContext = _vm = new ProfileViewModel();
	}
}