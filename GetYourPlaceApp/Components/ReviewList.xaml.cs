using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class ReviewList : ContentView
{
	public ReviewList()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty ReviewsProperty = BindableProperty.Create(
      nameof(Reviews),
      typeof(ObservableCollection<GYPReview>),
      typeof(ReviewList)
      );
    #endregion


    #region [Properties]
    public ObservableCollection<GYPReview> Reviews
    {
        get => (ObservableCollection<GYPReview>)this.GetValue(ReviewsProperty);
        set => this.SetValue(ReviewsProperty, value);
    }

    #endregion

}