using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components.ViewModels;

namespace GetYourPlaceApp.Components;

public partial class FilterEditPopUp : Popup
{
	public FilterEditPopUp()
	{
		InitializeComponent();
		BindingContext = new FilterEditPopUpViewModel(this);
	}

}