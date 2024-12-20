using System.Windows.Input;

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

    public static readonly BindableProperty HeartClickCommandProperty = BindableProperty.Create(
    nameof(HeartClickCommand),
    typeof(ICommand),
    typeof(HeartView)
    );

    public static readonly BindableProperty PropertyIdProperty = BindableProperty.Create(
    nameof(PropertyId),
    typeof(int),
    typeof(HeartView)
    );
    #endregion


    #region [Properties]
    public bool Liked
    {
        get => (bool)this.GetValue(LikedProperty);
        set => this.SetValue(LikedProperty, value);
    }

    public int PropertyId
    {
        get => (int)this.GetValue(PropertyIdProperty);
        set => this.SetValue(PropertyIdProperty, value);
    }

    public ICommand HeartClickCommand
    {
        get => (ICommand)this.GetValue(HeartClickCommandProperty);
        set => this.SetValue(HeartClickCommandProperty, value);
    }

    #endregion

    [RelayCommand]
    public async Task HeartClicked()
    {
        Liked = !Liked;
        HeartClickCommand?.Execute(new Tuple<bool,int>(Liked,PropertyId));
    }
}