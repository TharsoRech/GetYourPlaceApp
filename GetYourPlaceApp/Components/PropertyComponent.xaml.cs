using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Models;
using System.Reflection;
using System.Windows.Input;

namespace GetYourPlaceApp.Components;

public partial class PropertyComponent : ContentView
{
	public PropertyComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty PropertyProperty = BindableProperty.Create(
      nameof(Property),
      typeof(Property),
      typeof(PropertyComponent)
      );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(PropertyComponent)
    );


    public static readonly BindableProperty HeartClickCommandProperty = BindableProperty.Create(
    nameof(HeartClickCommand),
    typeof(ICommand),
    typeof(PropertyComponent)
    );

    #endregion

    #region [Properties]
    public Property Property 
    { 
        get => (Property)this.GetValue(PropertyProperty);
        set => this.SetValue(PropertyProperty, value);
    }

    public ICommand HeartClickCommand
    {
        get => (ICommand)this.GetValue(HeartClickCommandProperty);
        set => this.SetValue(HeartClickCommandProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }

    #endregion

    [RelayCommand]
    public async Task GoToPropertyInfo(Property property)
    {
        var loading = new LoadingPopUpPage();
        loading.ShowLoading();
        await Navigation.PushAsync(new PropertyDetail(property));
        loading.HideLoading();
    }
}