using CommunityToolkit.Maui.Views;

namespace GetYourPlaceApp.Components;

public partial class LoadingPopUpPage : Popup
{
	public LoadingPopUpPage()
	{
		InitializeComponent();
	}

    #region Bindable Properties

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
      nameof(IsLoading),
      typeof(bool),
      typeof(LoadingPopUpPage)
    );

    #endregion

    #region [Properties]

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }
    #endregion

    public void ShowLoading()
    {
        IsLoading = true;
        Application.Current.MainPage.ShowPopupAsync(this);
    }

    public void HideLoading()
    {
        IsLoading = false;
        this.CloseAsync();
    }
}