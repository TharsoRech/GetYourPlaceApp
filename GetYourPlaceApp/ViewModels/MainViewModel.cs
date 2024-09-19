using CommunityToolkit.Maui.Views;
using GetYourPlaceApp.Components;

namespace GetYourPlaceApp.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        
    }
    [RelayCommand]
    public async Task ShowFilter()
    {

        await Application.Current.MainPage.ShowPopupAsync(new FilterPopUp());

    }
}
