namespace GetYourPlaceApp.Components;

public partial class LoadingComponent : ContentView
{
	public LoadingComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
      nameof(IsLoading),
      typeof(bool),
      typeof(LoadingComponent)
    );

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
    nameof(LoadingText),
    typeof(string),
    typeof(LoadingComponent)
    );


    public static readonly BindableProperty LoadingColorProperty = BindableProperty.Create(
    nameof(LoadingColor),
    typeof(Color),
    typeof(LoadingComponent)
    );

    #endregion

    #region [Properties]

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }

    public string LoadingText
    {
        get => (string)this.GetValue(LoadingTextProperty);
        set => this.SetValue(LoadingTextProperty, value);
    }

    public Color LoadingColor
    {
        get => (Color)this.GetValue(LoadingColorProperty);
        set => this.SetValue(LoadingColorProperty, value);
    }
    #endregion
}