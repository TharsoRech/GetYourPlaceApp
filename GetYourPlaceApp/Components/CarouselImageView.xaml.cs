using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class CarouselImageView : ContentView
{
	public CarouselImageView()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
      nameof(Images),
      typeof(List<ImageSource>),
      typeof(CarouselImageView),
      propertyChanged:PropertyUpdated
      );


    private static readonly BindableProperty PaginationItemsProperty = BindableProperty.Create(
    nameof(PaginationItems),
    typeof(ObservableCollection<PaginationItemImage>),
    typeof(CarouselImageView),
    defaultValue: new ObservableCollection<PaginationItemImage>()
    );

    private static readonly BindableProperty CurrentImageProperty = BindableProperty.Create(
    nameof(CurrentImage),
    typeof(ImageSource),
    typeof(CarouselImageView)
    );

    #endregion

    #region [Properties]
    public List<ImageSource> Images
    {
        get => (List<ImageSource>)this.GetValue(ImagesProperty);
        set => this.SetValue(ImagesProperty, value);
    }

    public ObservableCollection<PaginationItemImage> PaginationItems
    {
        get => (ObservableCollection<PaginationItemImage>)this.GetValue(PaginationItemsProperty);
        set => this.SetValue(PaginationItemsProperty, value);
    }

    public ImageSource CurrentImage
    {
        get => (ImageSource)this.GetValue(CurrentImageProperty);
        set => this.SetValue(CurrentImageProperty, value);
    }
    #endregion

    private static void PropertyUpdated(BindableObject bindable, object oldValue, object newValue)
    {
        var paginationComponent = (CarouselImageView)bindable;
        paginationComponent.GeneratePagination();
    }

    private void GeneratePagination()
    {
        try
        {
            if (Images?.Count > 0)
            {
                PaginationItems?.Clear();
                for (int i = 0; i < Images.Count; i++)
                {
                    PaginationItemImage paginationItem = new PaginationItemImage();
                    paginationItem.PageIndex = i;
                    paginationItem.Image = Images[i];

                    if (i == 0)
                        paginationItem.IsActive = true;


                    PaginationItems.Add(paginationItem);
                }
                CurrentImage = Images.FirstOrDefault();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

    [RelayCommand]
    public async Task OpenImage(PaginationItemImage paginationItem)
    {
        try
        {
            paginationItem.IsActive = true;
            CurrentImage = Images[paginationItem.PageIndex];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
}