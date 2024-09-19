using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components.ViewModels;

namespace GetYourPlaceApp.Components;

public partial class FilterPopUp : Popup
{
	public FilterPopUp()
	{
		InitializeComponent();
        BindingContext = new FilterPopUpViewModel(this);
    }
}
