namespace GetYourPlaceApp.Components;

public partial class HeartView : ContentView
{

    public HeartView()
    {
        InitializeComponent();
        BindingContext = this;
    }

    #region Bindable Properties
    public static readonly BindableProperty LikedProperty = BindableProperty.Create(
      nameof(Liked),
      typeof(bool),
      typeof(HeartView)
      );
    #endregion


    #region [Properties]
    public bool Liked
    {
        get => (bool)this.GetValue(LikedProperty);
        set => this.SetValue(LikedProperty, value);
    }

    #endregion

    [RelayCommand]
    public async Task HeartClicked()
    {
        Liked = !Liked;
    }
}