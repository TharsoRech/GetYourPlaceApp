using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Components;

public partial class PaginationComponent : ContentView
{
    #region Variables
    public int CountPages;
    public bool NextPageEnabledButton;
    public bool PreviousPageEnabledButton;
    public int CurrentPage;
    #endregion
    public PaginationComponent()
	{
		InitializeComponent();
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

    private static void PropertyUpdated(BindableObject bindable, object oldValue, object newValue)
    {
        var paginationComponent = (PaginationComponent)bindable;
        paginationComponent.GeneratePagination();
    }
    #endregion

    #region [Properties]
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
    #endregion
    private void GeneratePagination()
    {
        try
        {
            if(Items?.Count > 0)
            {
                PaginationItems?.Clear();
                CountPages = (Items.Count + NumberOfItemsPerPage - 1) / NumberOfItemsPerPage;
                for (int i = 1; i < CountPages + 1; i++)
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

            if (PaginationItems?.Count >= 1)
            {
                paginationComponent.IsVisible = true;
                PreviousPageEnabledButton = true;

                if(PaginationItems.Count > 1)
                    NextPageEnabledButton = true;
            }
            else
            {
                PreviousPageEnabledButton = false;
                paginationComponent.IsVisible = false;
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
}