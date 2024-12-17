namespace GetYourPlaceApp.Views;

public partial class FavoritesPage : ContentPage
{
	FavoritesViewModel _viewModel;
	public FavoritesPage()
	{
		InitializeComponent();
		BindingContext = _viewModel = new FavoritesViewModel();
    }

    protected override void OnAppearing()
    {
        _viewModel?.GetPropertiesInBackground();
        base.OnAppearing();
    }
}