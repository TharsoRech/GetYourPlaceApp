using GetYourPlaceApp.Managers;
using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class PaginationComponent : ContentView, IDisposable
{
    #region Variables
    public int CountPages;
    public int CurrentPage;
    #endregion
    public PaginationComponent()
	{
		InitializeComponent();
        FilterManager.Instance.FilterChangeOrder += FilterOrderUpdated;
        FilterManager.Instance.FilterUpdated += FilterUpdated;
    }
    #region Bindable Properties

    public static readonly BindableProperty NumberOfItemsPerPageProperty = BindableProperty.Create(
    nameof(NumberOfItemsPerPage),
    typeof(int),
    typeof(PaginationComponent),
    defaultValue: 4,
    propertyChanged: PropertyUpdated
    );

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
    nameof(Items),
    typeof(ObservableCollection<Property>),
    typeof(PaginationComponent),
    defaultValue: new ObservableCollection<Property>(),
    propertyChanged: PropertyUpdated
    );

    public static readonly BindableProperty NextPageExecuteProperty = BindableProperty.Create(
    nameof(NextPageExecute),
    typeof(IAsyncRelayCommand),
    typeof(PaginationComponent)
    );

    public static readonly BindableProperty PreviousPageExecuteProperty = BindableProperty.Create(
    nameof(PreviousPageExecute),
    typeof(IAsyncRelayCommand),
    typeof(PaginationComponent)
    );

    public static readonly BindableProperty PaginationItemsProperty = BindableProperty.Create(
    nameof(PaginationItems),
    typeof(ObservableCollection<PaginationItem>),
    typeof(PaginationComponent),
    defaultValue: new ObservableCollection<PaginationItem>()
    );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(PaginationComponent)
    );

    public static readonly BindableProperty NextPageEnabledButtonProperty = BindableProperty.Create(
    nameof(NextPageEnabledButton),
    typeof(bool),
    typeof(PaginationComponent)
    );


    public static readonly BindableProperty PreviousPageEnabledButtonProperty = BindableProperty.Create(
    nameof(PreviousPageEnabledButton),
    typeof(bool),
    typeof(PaginationComponent)
    );

    private static void PropertyUpdated(BindableObject bindable, object oldValue, object newValue)
    {
        var paginationComponent = (PaginationComponent)bindable;
        paginationComponent.GeneratePagination();
    }
    #endregion

    #region [Properties]

    public bool NextPageEnabledButton
    {
        get => (bool)this.GetValue(NextPageEnabledButtonProperty);
        set => this.SetValue(NextPageEnabledButtonProperty, value);
    }

    public bool PreviousPageEnabledButton
    {
        get => (bool)this.GetValue(PreviousPageEnabledButtonProperty);
        set => this.SetValue(PreviousPageEnabledButtonProperty, value);
    }
    public int NumberOfItemsPerPage
    {
        get => (int)this.GetValue(NumberOfItemsPerPageProperty);
        set => this.SetValue(NumberOfItemsPerPageProperty, value);
    }

    public ObservableCollection<Property> Items
    {
        get => (ObservableCollection<Property>)this.GetValue(ItemsProperty);
        set => this.SetValue(ItemsProperty, value);
    }

    public ObservableCollection<PaginationItem> PaginationItems
    {
        get => (ObservableCollection<PaginationItem>)this.GetValue(PaginationItemsProperty);
        set => this.SetValue(PaginationItemsProperty, value);
    }

    public IAsyncRelayCommand NextPageExecute
    {
        get => (IAsyncRelayCommand)this.GetValue(NextPageExecuteProperty);
        set => this.SetValue(NextPageExecuteProperty, value);
    }

    public IAsyncRelayCommand PreviousPageExecute
    {
        get => (IAsyncRelayCommand)this.GetValue(PreviousPageExecuteProperty);
        set => this.SetValue(PreviousPageExecuteProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }
    #endregion
    private void GeneratePagination()
    {
        try
        {
            if(Items?.Count > 0)
            {
                PaginationItems?.Clear();
                CountPages = (Items.Count + NumberOfItemsPerPage - 1) / NumberOfItemsPerPage;
                var calculateFirstPage = CurrentPage  >= CountPages ? CurrentPage -3 : CurrentPage - 2;
                var firstPage = CurrentPage == 0 || CurrentPage == 1 || calculateFirstPage  < 1 ? 1 : calculateFirstPage;
                var lastPage = ((firstPage) + 4) > (CountPages +1) ? CountPages : ((firstPage) + 4) ;
                for (int i = firstPage; i < lastPage; i++)
                {
                    PaginationItem paginationItem = new PaginationItem();
                    if (i == CurrentPage || CurrentPage == 0)
                        paginationItem.IsActive = true;

                    if (CurrentPage == 0)
                        CurrentPage = 1;

                    paginationItem.PageIndex= i;

                    PaginationItems.Add(paginationItem);

                }

            }

            if (PaginationItems?.Count > 0)
            {
                if(CurrentPage == CountPages)
                    NextPageEnabledButton = false;
                else
                    NextPageEnabledButton = true;

                if(CurrentPage == 1)
                    PreviousPageEnabledButton = false;
                else
                    PreviousPageEnabledButton = true;
            }
            else
            {
                PreviousPageEnabledButton = false;
                NextPageEnabledButton = false;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

    [RelayCommand]  
    public async Task NextPage()
    {
        CurrentPage += 1;
        OrganizeAndGoToPage(true);
    }

    [RelayCommand]
    public async Task PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage -= 1;
            OrganizeAndGoToPage(false);

        }
    }

    [RelayCommand]
    public async Task GoToPage(PaginationItem paginationItem)
    {
        if(paginationItem!= null && paginationItem.PageIndex > 0)
        {
            bool isNextPage = paginationItem.PageIndex > CurrentPage;
            CurrentPage = paginationItem.PageIndex;
            OrganizeAndGoToPage(isNextPage);
        }

    }

    public void OrganizeAndGoToPage(bool isNextPage)
    {
        GeneratePagination();
        if(isNextPage)
            NextPageExecute?.Execute(new Tuple<int, int>(CurrentPage, NumberOfItemsPerPage));
        else
            PreviousPageExecute?.Execute(new Tuple<int, int>(CurrentPage, NumberOfItemsPerPage));
    }

    private void FilterOrderUpdated(object sender, string filter)
    {
        CurrentPage = 1;
    }

    private void FilterUpdated(object sender, List<GYPPropertyInfoItem> filterItems)
    {
        CurrentPage = 1;
    }

    public void Dispose()
    { 
        FilterManager.Instance.FilterUpdated -= FilterUpdated;
        FilterManager.Instance.FilterChangeOrder -= FilterOrderUpdated;
    }
}