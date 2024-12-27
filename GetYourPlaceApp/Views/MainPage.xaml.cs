using GetYourPlaceApp.Components;

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
        Task.Factory.StartNew(() =>
        {
            Application.Current.Dispatcher.DispatchAsync(() =>
            {
                _mainViewModel?.GetPropertiesInBackground();
            });
        });

        base.OnAppearing();
    }
}
