using GetYourPlaceApp.Models;
using System.Windows.Input;

namespace GetYourPlaceApp.Components;

public partial class StarRatingComponent : ContentView
{
	public StarRatingComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty CanChangeStarsProperty = BindableProperty.Create(
    nameof(CanChangeStars),
    typeof(bool),
    typeof(StarRatingComponent)
    );
    public static readonly BindableProperty RatingProperty = BindableProperty.Create(
     nameof(Rating),
     typeof(int),
     typeof(StarRatingComponent),
     propertyChanged: OnStarChanged
     );
    public static readonly BindableProperty StarCountProperty = BindableProperty.Create(
    nameof(StarCount),
    typeof(int),
    typeof(StarRatingComponent),
    propertyChanged: OnStarChanged
    );

    public static readonly BindableProperty StarClickedCommandProperty = BindableProperty.Create(
    nameof(StarClickedCommand),
    typeof(ICommand),
    typeof(StarRatingComponent)
    );

    private static readonly BindableProperty StarListProperty = BindableProperty.Create(
    nameof(StarList),
    typeof(ObservableCollection<StarRating>),
    typeof(StarRatingComponent)
    );
    #endregion
    #region [Properties]

    public bool CanChangeStars
    {
        get => (bool)this.GetValue(CanChangeStarsProperty);
        set => this.SetValue(CanChangeStarsProperty, value);
    }

    public int Rating
    {
        get => (int)this.GetValue(RatingProperty);
        set
        {
            this.SetValue(RatingProperty, value);
        }
    }

    public int StarCount
    {
        get => (int)this.GetValue(StarCountProperty);
        set
        {
            this.SetValue(StarCountProperty, value);
        }
    }

    public ObservableCollection<StarRating> StarList
    {
        get => (ObservableCollection<StarRating>)this.GetValue(StarListProperty);
        set
        {
            this.SetValue(StarListProperty, value);
        }
    }

    public ICommand StarClickedCommand
    {
        get => (ICommand)this.GetValue(StarClickedCommandProperty);
        set => this.SetValue(StarClickedCommandProperty, value);
    }
    #endregion

    private static void OnStarChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var obj = bindable as StarRatingComponent;
        if (obj != null && oldValue != newValue)
        {
            obj.ResetStarRatingBackgroundColor();
        }

    }

    public void ResetStarRatingBackgroundColor()
    {
        if(StarList is null)
            StarList = new ObservableCollection<StarRating>();

        StarList?.Clear();
        for (int i = 1; i <= StarCount; i++)
        {
            if((i -1) < Rating)
            {
                StarList.Add(new StarRating
                {
                    Index = i,
                    StarColor = Color.FromArgb("#FFED4E")
                });
            }
            else
            {
                StarList.Add(new StarRating
                {
                    Index = i,
                    StarColor = Color.FromArgb("#CFCFCF")
                });
            }

        }
    }

    [RelayCommand]
    private void StarRatingClicked(StarRating starRating)
    {
        ResetStarRatingBackgroundColor();
        for (int i = 0; Rating < starRating.Index; i++)
        {
            StarList[i].StarColor = Color.FromArgb("#FFED4E");
        }
        StarClickedCommand?.Execute(this);
    }

}