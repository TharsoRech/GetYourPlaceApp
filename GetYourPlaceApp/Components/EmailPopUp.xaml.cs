using CommunityToolkit.Maui.Views;

namespace GetYourPlaceApp.Components;

public partial class EmailPopUp : Popup
{
	public EmailPopUp()
	{
		InitializeComponent();
	}



	[RelayCommand]
	public async Task Close()
	{
		this.CloseAsync();
	}

    [RelayCommand]
    public async Task Send()
    {
        Application.Current.MainPage.ShowPopupAsync(new MessagePopUpComponent(
			"Email Sent",
			"We sent an email of recover in your email.",
			"Ok",
			new Command(() => this.CloseAsync())));
    }
}