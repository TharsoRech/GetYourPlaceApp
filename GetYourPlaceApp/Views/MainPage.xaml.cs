namespace GetYourPlaceApp.Views;

public partial class MainPage : ContentPage
{
    private MainViewModel _mainViewModel;
    public MainPage()
	{
		InitializeComponent();
        _mainViewModel = new MainViewModel();
        BindingContext = _mainViewModel;
    }

    protected override void OnAppearing()
    {
        _mainViewModel?.GetProperties();
        base.OnAppearing();
    }
}
