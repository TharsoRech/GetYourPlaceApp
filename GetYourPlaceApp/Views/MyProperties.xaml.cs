namespace GetYourPlaceApp.Views;

public partial class MyProperties : ContentPage
{
    MyPropertiesViewModel _viewModel;
    public MyProperties()
	{
		InitializeComponent();
        BindingContext = _viewModel = new MyPropertiesViewModel();
    }
}