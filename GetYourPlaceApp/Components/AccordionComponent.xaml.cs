using GetYourPlaceApp.Services.BackGroundTask;
using System.Windows.Input;

namespace GetYourPlaceApp.Components;

public partial class AccordionComponent : ContentView
{
	public AccordionComponent()
	{
		InitializeComponent();
	}

    #region Bindable Properties

    private static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
    nameof(IsLoading),
    typeof(bool),
    typeof(AccordionComponent)
    );

    private static readonly BindableProperty ExpandedProperty = BindableProperty.Create(
    nameof(Expanded),
    typeof(bool),
    typeof(AccordionComponent)
    );


    public static readonly BindableProperty ExpandCommandProperty = BindableProperty.Create(
    nameof(ExpandCommand),
    typeof(ICommand),
    typeof(AccordionComponent)
    );


    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
    nameof(Title),
    typeof(string),
    typeof(AccordionComponent)
    );


    public static readonly BindableProperty CustomContentProperty = BindableProperty.Create(
    nameof(CustomContent),
    typeof(View),
    typeof(AccordionComponent),
    propertyChanged: OnCustomContentChanged
    );

    #endregion

    #region [Properties]

    public ICommand ExpandCommand
    {
        get => (ICommand)this.GetValue(ExpandCommandProperty);
        set => this.SetValue(ExpandCommandProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)this.GetValue(IsLoadingProperty);
        set => this.SetValue(IsLoadingProperty, value);
    }

    public bool Expanded
    {
        get => (bool)this.GetValue(ExpandedProperty);
        set => this.SetValue(ExpandedProperty, value);
    }

    public string Title
    {
        get => (string)this.GetValue(TitleProperty);
        set => this.SetValue(TitleProperty, value);
    }

    public View CustomContent
    {
        get => (View)this.GetValue(CustomContentProperty);
        set => this.SetValue(CustomContentProperty, value);
    }

    #endregion

    private static void OnCustomContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (AccordionComponent)bindable;
        control.UpdateContent((View)newValue);
    }

    [RelayCommand]
    public async Task ExpandClicked()
    {
        Expanded = !Expanded;
        IsLoading = true;
        Task.Factory.StartNew(async () =>
        {
            await Application.Current.Dispatcher.DispatchAsync(async () =>
            {
                ExpandCommand?.Execute(null);
            });
            IsLoading = false;
        });

    }


    private void UpdateContent(View newContent)
    {
       customContent.Children.Clear();

       if (newContent != null)
         customContent.Children.Add(newContent);

        IsLoading = false;
    }
}