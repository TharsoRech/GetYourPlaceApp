using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components.ViewModels;
using System.Windows.Input;

namespace GetYourPlaceApp.Components;

public partial class MessagePopUpComponent : Popup
{
    MessagePopUpComponentViewModel _vm;
    public MessagePopUpComponent(string title, string subTitle, string message, ICommand messageClickOkCommand = null)
	{
		InitializeComponent();
		BindingContext = _vm = new MessagePopUpComponentViewModel(title, subTitle, message,messageClickOkCommand);

    }


    [RelayCommand]
    public async Task MessageClick()
    {
        _vm?.ExecuteClickOkCommand();
        CloseAsync();
    }
}