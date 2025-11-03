
public class IOSToastPlatformService : IToastPlatformService
{
    private UIView _toastView;

    public async Task ShowToastAsync(ContentView view)
    {
        if (view.Handler == null)
        {
            view.Handler = view.ToHandler(Application.Current.MainPage.Handler.MauiContext);
        }

        var nativeView = view.Handler.PlatformView as UIView;

        var parentViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (parentViewController == null)
            return;

        // Remove existing toast if any
        if (_toastView != null)
        {
            _toastView.RemoveFromSuperview();
            _toastView = null;
        }

        _toastView = nativeView;

        _toastView.TranslatesAutoresizingMaskIntoConstraints = false;
        parentViewController.View.AddSubview(_toastView);

        NSLayoutConstraint heightConstraint = new();
        var firstChild = _toastView.Subviews.FirstOrDefault();

        if (firstChild != null)
        {
            heightConstraint = _toastView.HeightAnchor.ConstraintGreaterThanOrEqualTo(firstChild.HeightAnchor);
        }
        else
        {
            heightConstraint = _toastView.HeightAnchor.ConstraintGreaterThanOrEqualTo(80);
        }

        // Set constraints to fill the width of the parent view and position at the top with padding
        NSLayoutConstraint.ActivateConstraints(new[]
        {
            _toastView.CenterXAnchor.ConstraintEqualTo(parentViewController.View.CenterXAnchor),
            _toastView.TopAnchor.ConstraintEqualTo(parentViewController.View.SafeAreaLayoutGuide.TopAnchor, 38),
            _toastView.WidthAnchor.ConstraintEqualTo(parentViewController.View.WidthAnchor),
            heightConstraint,
        });

        _toastView.Alpha = 0;
        UIView.Animate(0.3, () =>
        {
            _toastView.Alpha = 1;
        });
    }

    public void DismissPreemptively()
    {
        if (_toastView != null)
        {
            _toastView.RemoveFromSuperview();
            _toastView = null;
        }
    }

    public async Task DismissToastAsync()
    {
        if (_toastView != null)
        {
            UIView.Animate(0.3, () =>
            {
                _toastView.Alpha = 0;
            }, () =>
            {
                _toastView?.RemoveFromSuperview();
                _toastView = null;
            });
        }
    }
}

public class AndroidToastPlatformService : IToastPlatformService
{
    private Android.Views.View _toastView;

    public async Task ShowToastAsync(ContentView view)
    {
        if (view.Handler == null)
        {
            view.Handler = view.ToHandler(Application.Current.MainPage.Handler.MauiContext);
        }

        var nativeView = view.Handler.PlatformView as Android.Views.View;

        var activity = Application.Current.MainPage.Handler.MauiContext.Context.GetActivity();

        if (activity == null)
            return;

        // Remove existing toast if any
        if (_toastView != null)
        {
            var parentLayout = _toastView.Parent as ViewGroup;
            parentLayout?.RemoveView(_toastView);
            _toastView = null;
        }

        _toastView = nativeView;

        if (_toastView != null)
        {
            var layoutParams = new FrameLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent
            );

            layoutParams.Gravity = GravityFlags.Top;

            var decorView = activity.Window.DecorView as ViewGroup;
            decorView?.AddView(_toastView, layoutParams);

            decorView?.Post(() =>
            {
                var insets = decorView.RootWindowInsets;
                if (insets != null && _toastView != null)
                {
                    var topInset2 = insets.StableInsetTop;
                    layoutParams.TopMargin = topInset2 * 2;
                    _toastView.LayoutParameters = layoutParams;
                }
            });

            _toastView.Alpha = 0;
            _toastView.Animate().Alpha(1).SetDuration(300).Start();
        }
    }

    public async Task DismissToastAsync()
    {
        if (_toastView != null)
        {
            _toastView.Animate()
                .Alpha(0)
                .SetDuration(300)
                .WithEndAction(new Runnable(() =>
                {
                    if (_toastView != null)
                    {
                        var parent = _toastView.Parent as ViewGroup;
                        parent?.RemoveView(_toastView);
                        _toastView = null;
                    }
                }))
                .Start();
        }
    }

    public void DismissPreemptively()
    {
        if (_toastView != null)
        {
            (_toastView.Parent as ViewGroup)?.RemoveView(_toastView);
            _toastView = null;
        }
    }
}

#if ANDROID
        mauiAppBuilder.Services.AddSingleton<IToastPlatformService, AndroidToastPlatformService>();
#endif

#if IOS
          mauiAppBuilder.Services.AddSingleton<IToastPlatformService, IOSToastPlatformService>();
#endif

<?xml version="1.0" encoding="utf-8"?>

<ContentView xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:local="clr-namespace:Tke.Gta.Maui.UserControls.DesignSystem.Toast"
       x:DataType="local:DsToastMessage"
       BackgroundColor="{Binding ToastType, Converter={StaticResource DsToastTypeConverterColor}}"
       x:Name="DsToast"
       VerticalOptions="Start"
       x:Class="Tke.Gta.Maui.UserControls.DesignSystem.Toast.DsToastMessage">
    <ContentView.Resources>
        <ResourceDictionary>
            <local:DsToastTypeConverterColor x:Key="DsToastTypeConverterColor" />
            <local:DsToastTypeConverterImage x:Key="DsToastTypeConverterImage" />
        </ResourceDictionary>
    </ContentView.Resources>
    <Grid
          VerticalOptions="Start"
          HorizontalOptions="Fill"
          ColumnSpacing="{DynamicResource DsSpacingLarge}"
          ColumnDefinitions="*,Auto">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="{OnPlatform Android='*', iOS='Auto'}"  />
        </Grid.RowDefinitions>
        <Grid.Padding>
            <OnPlatform x:TypeArguments="Thickness">
                <On Platform="Android"  Value="{StaticResource DsSizingX3}" />
                <On Platform="iOS" Value="{StaticResource DsSizingX4}" />
            </OnPlatform>
        </Grid.Padding>
        <HorizontalStackLayout
            Spacing="{DynamicResource DsSpacingSmall}"
            Grid.Row="0">
            <Image
                   WidthRequest="20"
                   HeightRequest="20"
                   Source="{Binding ToastType, Converter={StaticResource DsToastTypeConverterImage}}">
            </Image>
            <Label
                TextColor="{Binding TextColor}"
                FontAttributes="Bold"
                LineBreakMode="WordWrap"
                FontSize="{StaticResource DsFontSize500}"
                Text="{Binding Title}">
            </Label>
        </HorizontalStackLayout>
        <Label
              Grid.Column="0"
              Grid.Row="1"
              HorizontalOptions="Fill"
              LineBreakMode="WordWrap"
              VerticalOptions="Start"
              FontSize="{StaticResource DsFontSize400}"
              TextColor="{Binding TextColor}"
              Text="{Binding Message}">
            <Label.Padding>
                <Thickness
                    Left="{StaticResource DsSizingX7}"/>
            </Label.Padding>
        </Label>
        <Image Grid.Row="0"
               IsVisible="{Binding ShowCloseButton}"
               Grid.Column="1"
               WidthRequest="24"
               HorizontalOptions="End"
               HeightRequest="24"
               Source="{Binding CloseImage}">
            <Image.GestureRecognizers>
                <TapGestureRecognizer Command="{Binding CloseCommand }"/>
            </Image.GestureRecognizers>
        </Image>
    </Grid>
</ContentView>

using CommunityToolkit.Mvvm.Input;

namespace Tke.Gta.Maui.UserControls.DesignSystem.Toast;

public partial class DsToastMessage:ContentView,IAsyncDisposable
{

    private static readonly BindableProperty MessageProperty =
        BindableProperty.Create(
            nameof(Message),
            typeof(string),
            typeof(DsToastMessage)
        );

    private static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(DsToastMessage)
        );

    private static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(
            nameof(TextColor),
            typeof(Color),
            typeof(DsToastMessage)
        );

    private static readonly BindableProperty ToastTypeProperty =
        BindableProperty.Create(
            nameof(ToastType),
            typeof(DsToastType),
            typeof(DsToastMessage)
        );

    public static readonly BindableProperty CloseImageProperty =
        BindableProperty.Create(
            nameof(CloseImage),
            typeof(ImageSource),
            typeof(DsToastMessage)
        );

    public static readonly BindableProperty ShowCloseButtonProperty =
        BindableProperty.Create(
            nameof(ShowCloseButton),
            typeof(bool),
            typeof(DsToastMessage)
        );

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        init => SetValue(MessageProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        init => SetValue(TitleProperty, value);
    }

    public DsToastType ToastType
    {
        get => (DsToastType)GetValue(ToastTypeProperty);
        init => SetValue(ToastTypeProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        init => SetValue(TextColorProperty, value);
    }

    public ImageSource CloseImage
    {
        get => (ImageSource)GetValue(CloseImageProperty);
        init => SetValue(CloseImageProperty, value);
    }

    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        init => SetValue(ShowCloseButtonProperty, value);
    }

    private readonly TimeSpan _duration;
    private IToastMessage _toast;
    private Timer _dismissTimer;
    private bool _autoDismiss;

    public DsToastMessage(string title, string message,TimeSpan duration,DsToastType toastType,
        bool showCloseButton = true,bool autoDismiss = true)
    {
        InitializeComponent();
        BindingContext = this;
        Title = title;
        Message = message;
        _duration = duration;
        ToastType = toastType;
        TextColor = toastType == DsToastType.Warning ? (Color)App.Current.Resources["DsGrayDarkGray300"] : (Color)App.Current.Resources["DsGlobalWhite"];
        CloseImage = toastType == DsToastType.Warning ? "ds_close_black.png" : "ds_close_white.png";
        _toast = new ToastMessage(this);
        _autoDismiss = autoDismiss;
        ShowCloseButton = showCloseButton;
    }

    public async Task Show()
    {
        await MainThread.InvokeOnMainThreadAsync((Func<Task>)(async () =>
        {
            await _toast.ShowToast();
        }));

        _dismissTimer?.Dispose();
        if (_autoDismiss)
        {
            _dismissTimer = new Timer(async _ =>
            {
                await MainThread.InvokeOnMainThreadAsync((Func<Task>)(async () =>
                {
                    await _toast?.DismissToast();
                }));
            }, null, _duration, Timeout.InfiniteTimeSpan);
        }
    }

    [RelayCommand]
    private async Task Close()
    {
        await _toast?.DismissToast();
    }


    public async ValueTask DisposeAsync()
    {
        if (_dismissTimer != null)
        {
            await _dismissTimer.DisposeAsync();
        }
    }
}

public interface IToastMessage
{
    Task ShowToast();

    Task DismissToast();
}

public interface IToastPlatformService
{
    Task ShowToastAsync(ContentView view);
    Task DismissToastAsync();
}

ublic class ToastMessage: IToastMessage
{
    ContentView view;
    public ToastMessage(ContentView toastContent)
    {
        view = toastContent;
    }
    public async Task ShowToast()
    {
        var toastService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IToastPlatformService>();
        if (toastService != null)
        {
            await toastService.ShowToastAsync(view);
        }
    }

    public async Task DismissToast()
    {
        var toastService = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IToastPlatformService>();
        if (toastService != null)
        {
            await toastService.DismissToastAsync();
        }
    }
}



