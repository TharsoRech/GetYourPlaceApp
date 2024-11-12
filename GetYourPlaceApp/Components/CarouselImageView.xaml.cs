using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class CarouselImageView : ContentView
{
	public CarouselImageView()
	{
		InitializeComponent();
	}

    #region Bindable Properties
    public static readonly BindableProperty ImagesBase64Property = BindableProperty.Create(
      nameof(ImagesBase64),
      typeof(List<string>),
      typeof(CarouselImageView),
      propertyChanged:PropertyUpdated
      );


    private static readonly BindableProperty PaginationItemsProperty = BindableProperty.Create(
    nameof(PaginationItems),
    typeof(ObservableCollection<PaginationItemImage>),
    typeof(CarouselImageView),
    defaultValue: new ObservableCollection<PaginationItemImage>()
    );

    private static readonly BindableProperty CurrentImageBase64Property = BindableProperty.Create(
    nameof(CurrentImageBase64),
    typeof(string),
    typeof(CarouselImageView)
    );

    #endregion

    #region [Properties]
    public List<string> ImagesBase64
    {
        get => (List<string>)this.GetValue(ImagesBase64Property);
        set => this.SetValue(ImagesBase64Property, value);
    }

    public ObservableCollection<PaginationItemImage> PaginationItems
    {
        get => (ObservableCollection<PaginationItemImage>)this.GetValue(PaginationItemsProperty);
        set => this.SetValue(PaginationItemsProperty, value);
    }

    public string CurrentImageBase64
    {
        get => (string)this.GetValue(CurrentImageBase64Property);
        set => this.SetValue(CurrentImageBase64Property, value);
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
            if (ImagesBase64?.Count > 0)
            {
                PaginationItems?.Clear();
                for (int i = 0; i < ImagesBase64.Count; i++)
                {
                    PaginationItemImage paginationItem = new PaginationItemImage();
                    paginationItem.PageIndex = i;
                    paginationItem.ImageBase64 = ImagesBase64[i];

                    if (i == 0)
                        paginationItem.IsActive = true;


                    PaginationItems.Add(paginationItem);
                }
                CurrentImageBase64 = ImagesBase64[0];
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
            var pagItemList = PaginationItems.ToList();
            var pgItem = pagItemList.FirstOrDefault(p => p.PageIndex == paginationItem.PageIndex);
            if (pgItem != null)
            {
                foreach (var pagItem in pagItemList.Where(p => p.PageIndex != paginationItem.PageIndex))
                    pagItem.IsActive = false;

                pgItem.IsActive = true;
                PaginationItems = new ObservableCollection<PaginationItemImage>(pagItemList);
                CurrentImageBase64 = ImagesBase64[pgItem.PageIndex];
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
}