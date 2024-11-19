using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class ReviewComponent : ContentView
{
	public ReviewComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty ReviewProperty = BindableProperty.Create(
      nameof(Review),
      typeof(GYPReview),
      typeof(ReviewComponent)
      );
    #endregion


    #region [Properties]
    public GYPReview Review
    {
        get => (GYPReview)this.GetValue(ReviewProperty);
        set => this.SetValue(ReviewProperty, value);
    }

    #endregion
}