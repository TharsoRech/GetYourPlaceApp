using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Views;

public partial class PropertyDetail : ContentPage
{
    PropertyDetailViewModel _viewModel;
    public PropertyDetail(Property property)
	{
		InitializeComponent();
		BindingContext = _viewModel = new PropertyDetailViewModel(property);

    }
}