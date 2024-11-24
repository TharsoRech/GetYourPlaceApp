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

    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                var filterItem = (string)picker.ItemsSource[selectedIndex];

                if (filterItem != null)
                    ApplyPropertiesByOrder(filterItem);


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }

    public async Task<bool> ApplyPropertiesByOrder(string filterItem)
    {
        try
        {
            if (filterItem != null)
            {
                if (filterItem.Contains("Most Recent (High to Low)"))
                    Reviews = new ObservableCollection<GYPReview>(Reviews.OrderByDescending(p => p.PuplishedAt).ToList());

                if (filterItem.Contains("Most Recent (Low to High)"))
                    Reviews = new ObservableCollection<GYPReview>(Reviews.OrderBy(p => p.PuplishedAt).ToList());

                if (filterItem.Contains("Rating (High to Low)"))
                    Reviews = new ObservableCollection<GYPReview>(Reviews.OrderByDescending(p => p.Stars).ToList());

                if (filterItem.Contains("Rating (Low to High)"))
                    Reviews = new ObservableCollection<GYPReview>(Reviews.OrderBy(p => p.Stars).ToList());

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

        return true;
    }

}