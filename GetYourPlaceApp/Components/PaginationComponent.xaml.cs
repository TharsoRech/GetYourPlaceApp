using GetYourPlaceApp.Models;
using System.Windows.Input;

namespace GetYourPlaceApp.Components;

public partial class PaginationComponent : ContentView
{
    #region Variables
    public int CountPages;
    public bool PreviousPageActive;
    public int PreviousPageText;
    public bool NextPageEnabled;
    public bool NextPageActive;
    public int NextPageText;
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
    typeof(ObservableCollection<object>),
    typeof(PaginationComponent),
    defaultValue: new ObservableCollection<object>(),
    propertyChanged: PropertyUpdated
    );

    public static readonly BindableProperty NextPageExecuteProperty = BindableProperty.Create(
    nameof(NextPageExecute),
    typeof(ICommand),
    typeof(PaginationComponent)
    );

    public static readonly BindableProperty PreviousPageExecuteProperty = BindableProperty.Create(
    nameof(PreviousPageExecute),
    typeof(ICommand),
    typeof(PaginationComponent)
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

    public ObservableCollection<object> Items
    {
        get => (ObservableCollection<Object>)this.GetValue(NumberOfItemsPerPageProperty);
        set => this.SetValue(NumberOfItemsPerPageProperty, value);
    }

    public ICommand NextPageExecute
    {
        get => (ICommand)this.GetValue(NextPageExecuteProperty);
        set => this.SetValue(NextPageExecuteProperty, value);
    }

    public ICommand PreviousPageExecute
    {
        get => (ICommand)this.GetValue(PreviousPageExecuteProperty);
        set => this.SetValue(PreviousPageExecuteProperty, value);
    }
    #endregion
    private void GeneratePagination()
    {
        CountPages = Items.Count / NumberOfItemsPerPage;
        for(int i = 0; i < 2; i++)
        {
            PaginationItem paginationItem = new PaginationItem();
            if (i == CurrentPage)
                paginationItem.IsActive = true;

            paginationItem.PageIndex = CurrentPage + i;

        }


        PreviousPageText = CurrentPage;

        NextPageText = CurrentPage + 2;

        PreviousPageActive = CurrentPage == PreviousPageText;

        NextPageActive = CurrentPage == NextPageText;

        if (CurrentPage > 1)
            PreviousPageEnabledButton = true;
        else
            PreviousPageEnabledButton = false;

        if (CountPages > 1 && CurrentPage < CountPages)
            NextPageEnabledButton = true;
        else
            NextPageEnabledButton = false;

        if (NextPageText > CountPages)
            NextPageEnabled = false;
        else
            NextPageEnabled = true;

    }

    [RelayCommand]  
    public async Task NextPage()
    {
        CurrentPage += 1;
        NextPageExecute?.Execute(NumberOfItemsPerPage);
    }

    [RelayCommand]
    public async Task PreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage -= 1;
            PreviousPageExecute?.Execute(NumberOfItemsPerPage);
        }
    }
}